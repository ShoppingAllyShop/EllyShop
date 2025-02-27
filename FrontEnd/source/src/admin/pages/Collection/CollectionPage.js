import React, { useEffect, useState } from "react";
import TopTitle from "../../common/components/TopTitle";
import AlertComponent from "../../../components/AlertComponent";
import { STATUS_RESPONSE_HTTP_ENUM } from "../../../constants/enum";
import {
  BUTTON_NAME,
  BUTTON_TYPE,
  RESPONSE_API_STATUS,
  SORT_TYPE,
} from "../../../constants/common";
import Loading from "../../../components/Loading";
import { useDispatch, useSelector } from "react-redux";
import useAxiosBase from "../../../api/useAxiosBase";
import { ADMIN_ENDPOINT } from "../../../constants/endpoint";
import {
  setCollectionData,
  setCreateUpdateCollectionModal,
  setData,
  setSearchInput,
} from "../../../redux/slices/admin/collectionSlice";
import { collectionTableHeader } from "./constants/dataUI";
import CreateUpdateCollectionFormModal from "./components/CreateUpdateCollectionFormModal";
import TopPaging from "../../common/components/paging/TopPaging";
import BottomPaging from "../../common/components/paging/BottomPaging";
import useAxiosAuth from "../../../api/useAxiosAuth";

const CollectionPage = () => {
  const axiosAuth = useAxiosAuth();
  const dispatch = useDispatch();
  const [isLoading, setIsLoading] = useState(false);
  const {alert, data } = useSelector((state) => state?.adminCollection);
  const createUpdateCollectionModal = useSelector(
    (state) => state?.adminCollection?.createUpdateCollectionModal
  );
  const paging = useSelector(state => state?.adminCollection?.data?.collectionData?.paging)
  const collectionList = useSelector((state) => state?.adminCollection?.data?.collectionData?.collectionList);
  const searchInputData =  useSelector(state => state?.adminCollection?.searchInput)

  const callSearchCollection = async (params) => {
    const endpoint = ADMIN_ENDPOINT.SEARCH_COLLECION;
    const [searchCollectionResponse] = await Promise.all([
      axiosAuth.get(endpoint, { params }),
    ]);
    
    if (
      searchCollectionResponse?.status === STATUS_RESPONSE_HTTP_ENUM.OK &&
      searchCollectionResponse?.data.status === RESPONSE_API_STATUS.SUCCESS
    ) {
      dispatch(setCollectionData(searchCollectionResponse.data.result.collectionData));
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
    callSearchCollection(params);
  };

  const handleClickEdit = (e) => {
    const collectionDataById = collectionList.find(
      (x) => x.id === e.target.getAttribute("id")
    );
    let modalData = {
      collectionData: collectionDataById,
      name: data.name,
      description: data.description
    };
    dispatch(
      setCreateUpdateCollectionModal({
        isShow: true,
        type: BUTTON_TYPE.UPDATE,
        data: modalData,
      })
    );
  };
  useEffect(() => {
    fetchData();
    window.scrollTo(0, 0);
  }, []);

  const fetchData = async () => {
    const endpoint = ADMIN_ENDPOINT.COLLECTION_INDEX;
    setIsLoading(true);
    const [collectionResponse] = await Promise.all([axiosAuth.get(endpoint)]);
    if (
      collectionResponse.status === STATUS_RESPONSE_HTTP_ENUM.OK &&
      collectionResponse.data.status === RESPONSE_API_STATUS.SUCCESS
    ) {
      //dispatch store
      setIsLoading(false);
      dispatch(setData(collectionResponse.data.result));
    }
  };
  const renderTable = () => {
    return (
      <section className="bg-gray-50 dark:bg-gray-900">
        <div className="">
          <div className="bg-white dark:bg-gray-800 relative shadow-md sm:rounded-lg overflow-hidden">
            <div className="overflow-x-auto">
              <table className="w-full text-sm text-left text-gray-500 dark:text-gray-400 table-fixed">
                <thead className="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                  <tr className="">
                    {collectionTableHeader.map((item) => {
                      return (
                        <th
                          className={`px-4 py-3 ${item.className}`}
                          key={item.id}
                        >
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
                  {collectionList?.map((item, index) => {

                    return (
                      <tr
                        key={item.id}
                        className="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-600"
                      >
                        <th className="px-4 py-3 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                          {index + 1}
                        </th>
                        <th className="px-4 py-3 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                          {item.name}
                        </th>
                        <th className="px-4 py-3 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                          {item.description}
                        </th>
                        <td className="px-6 py-4">
                          <button
                            className="font-semibold text-blue-600 dark:text-blue-500 hover:underline"
                            id={item.id}
                            onClick={handleClickEdit}
                            type="update"
                          > 
                            {BUTTON_NAME.UPDATE}
                          </button>
                        </td>
                      </tr>
                    );
                  })}
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </section>
    );
  };

  return (
    <div
      id="Collection-page"
      className="mx-auto max-w-screen-2xl p-4 md:p-6 2xl:p-10"
    >
    <TopTitle title="Bộ Sưu Tập" />
      <div className="flex flex-col gap-5 md:gap-7 2xl:gap-10">
        <div className="rounded-sm border border-stroke bg-white shadow-default dark:border-strokedark dark:bg-boxdark">
          <div className="data-table-common data-table-two max-w-full overflow-x-auto">
            <div className="datatable-wrapper datatable-loading no-footer sortable searchable fixed-columns">
              {paging && 
                  <TopPaging
                    data={data}
                    paging={paging} 
                    onSearch={(params) => callSearchCollection(params)}
                  />}
              <div className="datatable-container">{renderTable()}</div>
              {paging && paging?.totalPages > 0 && 
                <BottomPaging
                  paging={paging}
                  onSearch={(params) => callSearchCollection(params)}
                />
              }
            </div>
          </div>
        </div>
      </div>
      {isLoading && <Loading />}
      {createUpdateCollectionModal?.isShow && (
        <CreateUpdateCollectionFormModal
          data={createUpdateCollectionModal?.data}
           onCancelCollectionModal={() => dispatch(setCreateUpdateCollectionModal({isShow: false}))}     
          type={createUpdateCollectionModal?.type}
        />
      )}
      {alert?.isDisplay && (
        <AlertComponent
          isShow={alert.isDisplay}
          content={alert.content}
          type={alert.type}
        />
      )}
      {isLoading && <Loading />}
    </div>
  );
};

export default CollectionPage;
