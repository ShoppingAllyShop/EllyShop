import React, { useEffect, useState } from "react";
import TopTitle from "../../common/components/TopTitle";
import CategorySelectList from "./components/CategorySelectList";
import { cateData } from "./data";
import Loading from "../../../components/Loading";
import useAxiosBase from "../../../api/useAxiosBase";
import { PUPLIC_ENDPOINT } from "../../../constants/endpoint";
import { useDispatch, useSelector } from "react-redux";
import { setData, setAlert } from "../../../redux/slices/admin/categorySlice";
import { STATUS_RESPONSE_HTTP_ENUM } from "../../../constants/enum";
import { RESPONSE_API_STATUS } from "../../../constants/common";
import AlertComponent from "../../../components/AlertComponent";

const CategoryPage = () => {
  const axiosBase = useAxiosBase();
  const dispatch = useDispatch();
  const [isLoading, setIsLoading] = useState(false);
  const { alert } = useSelector(state => state.adminCategory)
  //const [cateResult, setCateResult] = useState({isDisplay: false, content:"", type:""});

  useEffect(() => {
    fetchData();
  }, []); 

  const fetchData = async () => {
    const endpoint = PUPLIC_ENDPOINT.CATEGORY_GET_ALL;
    setIsLoading(true);
    const [categoryResponse] = await Promise.all([axiosBase.get(endpoint)]);    

    if (
      categoryResponse.status === STATUS_RESPONSE_HTTP_ENUM.OK &&
      categoryResponse.data.status === RESPONSE_API_STATUS.SUCCESS
    ) {
      //dispatch store
      setIsLoading(false);
      dispatch(setData(categoryResponse.data.result));
    }
  };
//<div className="mx-auto max-w-screen-2xl p-4 md:p-6 2xl:p-10">
  return (
    <div className="mx-auto max-w-screen-2xl p-4 md:p-6 2xl:p-10">
      <TopTitle title="Danh mục sản phẩm" />
      <CategorySelectList data={cateData} />
      {isLoading && <Loading />}
      {alert.isDisplay && <AlertComponent isShow={alert.isDisplay} content={alert.content} type={alert.type}/>} 
    </div>
  );
};

export default CategoryPage;
