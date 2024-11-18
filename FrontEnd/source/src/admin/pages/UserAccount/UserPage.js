import React, { useEffect, useState } from "react";
import { userTableHeader } from "../../pages/UserAccount/constants/dataUI";
import { ADMIN_ENDPOINT } from "../../../constants/endpoint";
import { STATUS_RESPONSE_HTTP_ENUM } from "../../../constants/enum";
import { BUTTON_NAME, BUTTON_TYPE, RESPONSE_API_STATUS, SORT_TYPE } from "../../../constants/common";
import useAxiosAuth from "../../../api/useAxiosAuth";
import Loading from "../../../components/Loading";
import TopPaging from "../../common/components/paging/TopPaging";
import BottomPaging from "../../common/components/paging/BottomPaging";
import CreateUpdateUserFormModal from "./components/CreateUpdateUserFormModal";
import { useDispatch, useSelector } from "react-redux";
import { setCreateUpdateUserModal, setData, setSearchInput, setUserData } from "../../../redux/slices/admin/userSlice";
import AlertComponent from "../../../components/AlertComponent";

const UserPage = () => {
  const dispatch = useDispatch()
  const axiosAuth = useAxiosAuth();
  const [isLoading, setIsLoading] = useState(false);
  const createUpdateUserModal = useSelector((state) => state?.adminUserPage?.createUpdateUserModal);
  const { alert, data } = useSelector(state => state?.adminUserPage)
  const userList = useSelector(state => state?.adminUserPage?.data?.userData?.userList)
  const paging = useSelector(state => state?.adminUserPage?.data?.userData?.paging)
  const searchInputData =  useSelector(state => state?.adminUserPage?.searchInput)
  
  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = async () => {
    setIsLoading(true);
    getIndex();
    setIsLoading(false);
  };

  const getIndex = async () => {
    const endpoint = ADMIN_ENDPOINT.USER_INDEX;
    const [indexResponse] = await Promise.all([
      axiosAuth.get(endpoint),
    ]);

    if (
      indexResponse?.status === STATUS_RESPONSE_HTTP_ENUM.OK &&
      indexResponse?.data.status === RESPONSE_API_STATUS.SUCCESS
    ) {
      dispatch(setData(indexResponse.data.result));
    }
  };

  const callSearchEmployeeUser = async (params) => {
    const endpoint = ADMIN_ENDPOINT.SEARCH_EMPLOYEE_USER;
    const [searchUserResponse] = await Promise.all([
      axiosAuth.get(endpoint, { params }),
    ]);
    
    if (
      searchUserResponse?.status === STATUS_RESPONSE_HTTP_ENUM.OK &&
      searchUserResponse?.data.status === RESPONSE_API_STATUS.SUCCESS
    ) {
      dispatch(setUserData(searchUserResponse.data.result.userData));
      dispatch(setSearchInput(params.searchInput))
    }
  };

  const handleSetSearchParams = (sortName) => {
    let params = {
      pageNumber: paging?.pageNumber,
      pageSize: paging?.pageSize,
      searchInput: searchInputData,
      sortBy: sortName,
      sortOrder: paging?.sortOrder === SORT_TYPE.ASCENDING ? SORT_TYPE.DESCENDING : SORT_TYPE.ASCENDING,
    };

    //call back parent
    callSearchEmployeeUser(params);
  };

  const handleClickEdit = (e) => {
    const userDataById = userList.find(
      (x) => x.userId === e.target.getAttribute("user-id")
    );

    let modalData = {
      userData: userDataById,
      positions: data.positions,
      departments: data.departments,
      roles: data.roles
    }
    dispatch(setCreateUpdateUserModal({isShow: true, type: BUTTON_TYPE.UPDATE, data: modalData}));
  };

  const renderTable = () => {
    let startIndex = (paging?.pageNumber - 1) * paging?.pageSize;
    return (
      <section className="bg-gray-50 dark:bg-gray-900">
        <div className="">
          <div className="bg-white dark:bg-gray-800 relative shadow-md sm:rounded-lg overflow-hidden">               
            <div className="overflow-x-auto">           
              {userList?.length === 0 && <div className="text-blue-500 italic p-4 w-full text-center">Không tìm thấy</div>}
              {userList?.length > 0  &&
                <table className="w-full text-sm text-left text-gray-500 dark:text-gray-400 table-fixed">
                  <thead className="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                    <tr className="">
                      {userTableHeader.map((item) => {
                        return (
                          <th className={`px-4 py-3 ${item.className}`} key={item.id}>
                            <p className="float-left mr-2">{item.name}</p>
                            {item.name && item.propName !== '' && (
                              <i
                                className="fa-solid fa-sort text-xs float-left cursor-pointer hover:text-gray-500"
                                onClick={() =>handleSetSearchParams(item.propName)}
                              ></i>
                            )}
                          </th>
                        );
                      })}
                    </tr>
                  </thead>
                  <tbody>
                    {userList?.map((item) => {
                        startIndex++;
                        return (
                          <tr key={item.userId} className="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-600">
                            <th className="px-4 py-3 font-medium text-gray-900 whitespace-nowrap dark:text-white">{startIndex}</th>
                            <th className="px-4 py-3 font-medium text-gray-900 whitespace-nowrap dark:text-white">{item.userName}</th>
                            <td className="px-4 py-3 break-words">{item.email}</td>
                            <td className="px-4 py-3">{item.roleName}</td>
                            <td className="px-4 py-3">{item.position}</td>
                            <td className="px-4 py-3">{item.department}</td>
                            <td className="px-6 py-4">
                              <button className="font-semibold text-blue-600 dark:text-blue-500 hover:underline"
                                      user-id={item.userId}
                                      onClick={handleClickEdit}
                                      type="update"
                              >{BUTTON_NAME.UPDATE}</button>
                            </td>
                          </tr>
                        );
                      })}                   
                  </tbody>               
                </table>
              }             
            </div>
          </div>
        </div>
      </section>
    );
  };  

  return (
    <div id="user-page" className="mx-auto max-w-screen-2xl p-4 md:p-6 2xl:p-10">
      <div className="flex flex-col gap-5 md:gap-7 2xl:gap-10">
        <div className="rounded-sm border border-stroke bg-white shadow-default dark:border-strokedark dark:bg-boxdark">
          <div className="data-table-common data-table-two max-w-full overflow-x-auto">
            <div className="datatable-wrapper datatable-loading no-footer sortable searchable fixed-columns">
              {paging && 
                  <TopPaging
                    data={data}
                    paging={paging} 
                    onSearchEmployeeUser={(params) => callSearchEmployeeUser(params)}
                  />}
              <div className="datatable-container">{renderTable()}</div>
              {paging && paging?.totalPages > 0 && 
                <BottomPaging
                  paging={paging}
                  onSearchEmployeeUser={(params) => callSearchEmployeeUser(params)}
                />
              }
            </div>
          </div>
        </div>
      </div>
      {isLoading && <Loading />}
      {createUpdateUserModal?.isShow && (
        <CreateUpdateUserFormModal 
            data={createUpdateUserModal?.data} 
            onCancelUserModal={() => dispatch(setCreateUpdateUserModal({isShow: false}))}
            type={createUpdateUserModal?.type}/>
      )}
      {alert?.isDisplay && <AlertComponent isShow={alert.isDisplay} content={alert.content} type={alert.type}/>}
    </div>
  );
};

export default UserPage;
