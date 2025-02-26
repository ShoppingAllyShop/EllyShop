import React, { useEffect, useState } from "react";
import Header from "./Header";
import Footer from "./Footer";
import { Outlet } from "react-router-dom";
import Jornal from "./Jornal";
import { CLIENT_ENDPOINT } from "../../constants/endpoint";
import { STATUS_RESPONSE_HTTP_ENUM } from "../../constants/enum";
import { RESPONSE_API_STATUS } from "../../constants/common";
import useAxiosBase from "../../api/useAxiosBase";
import { useDispatch} from "react-redux";
import Loading from "../../components/Loading";

const Mainlayout = () => {
  const axiosBase = useAxiosBase();
  const dispatch = useDispatch();
  const [data, setLayOutData] = useState(null);
  const [isLoading, setIsLoading] = useState(false);
  
  //Chay vo đây 1 lần, sau lần render đầu tiên
  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = async () => {
    const contentMainPageEndpoint = CLIENT_ENDPOINT.MAIN_PAGE_CONTENT;
    const catalogEndpoint = CLIENT_ENDPOINT.CATALOG_CONTENT;
    setIsLoading(true);
    const [contentMainPageResponse, catalogResponse] = await Promise.all([
      axiosBase.get(contentMainPageEndpoint),
      axiosBase.get(catalogEndpoint),
    ]);
    if (
      contentMainPageResponse.status === STATUS_RESPONSE_HTTP_ENUM.OK &&
      contentMainPageResponse.data.status === RESPONSE_API_STATUS.SUCCESS &&
      catalogResponse.status === STATUS_RESPONSE_HTTP_ENUM.OK &&
      catalogResponse.data.status === RESPONSE_API_STATUS.SUCCESS
    ) {
      //dispatch store
      setIsLoading(false);

      //handle layout data
      const { headerData, footerData } = contentMainPageResponse.data.result.layoutData;
      const {newsMediaList} = contentMainPageResponse.data.result.mainPageData
      const { categoryList, collectionsList } = catalogResponse.data.result;
      var layoutdata = {categoryList,collectionsList, headerData, footerData, newsMediaList};
      setLayOutData(layoutdata);
    }
  };

  return (
    <div>
      {data && <Header data={data} />}
      <main>
        <Outlet />
      {data && <Jornal data={data.newsMediaList} />}
      </main>
      {data && <Footer data={data.footerData} />}
      {isLoading && <Loading />}
    </div>
  );
};

export default Mainlayout;
