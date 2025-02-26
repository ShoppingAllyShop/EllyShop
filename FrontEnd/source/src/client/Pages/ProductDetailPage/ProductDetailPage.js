import React, { useState, useEffect } from "react";
import BreadCrum from "./Components/BreadCrum";
import ShowRoom from "./Components/ShowRoom";
import Infomations from "./Components/Infomations";
import Prize from "./Components/Prize";
import ProductSharing from "./Components/ProductSharing";
import MultiTaps from "./Components/MultiTaps";
import { useParams } from "react-router-dom";
import { CLIENT_ENDPOINT } from "../../../constants/endpoint";
import useAxiosBase from "../../../api/useAxiosBase";
import { STATUS_RESPONSE_HTTP_ENUM } from "../../../constants/enum";
import { RESPONSE_API_STATUS } from "../../../constants/common";
import Loading from "../../../components/Loading";
import ProductListSlider from "./Components/ProductListSlider";
import ProductDetailSlider from "./Components/ProductDetailSlider";
import '../../Pages/ProductDetailPage/ProductDetail.scss'

const ProductDetailPage = () => {
  const { productId } = useParams();
  const axiosBase = useAxiosBase();
  const [data, setMainPageData] = useState(null);
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    fetchData();
    window.scrollTo(0, 0);
  }, [productId]);

  const fetchData = async () => {
    const contentMainPageEndpoint = CLIENT_ENDPOINT.MAIN_PAGE_CONTENT;
    const productEndpoint = `${CLIENT_ENDPOINT.PRODUCT_CONTENT}?id=${productId}`;
    setIsLoading(true);
    const [contentMainPageResponse, productResponse] = await Promise.all([
      axiosBase.get(contentMainPageEndpoint),
      axiosBase.get(productEndpoint),
    ]);
    if (
      contentMainPageResponse.status === STATUS_RESPONSE_HTTP_ENUM.OK &&
      contentMainPageResponse.data.status === RESPONSE_API_STATUS.SUCCESS &&
      productResponse.status === STATUS_RESPONSE_HTTP_ENUM.OK &&
      productResponse.data.status === RESPONSE_API_STATUS.SUCCESS
    ) {
      //dispatch store
      setIsLoading(false);
      //handle mainpage data
      const { branchList } = contentMainPageResponse.data.result.mainPageData;
      const {
        guaranteeList,
        guideList,
        newProducts,
        product,
        favourites,
        ratingList,
      } = productResponse.data.result;

      var productData = {
        guaranteeList,
        guideList,
        branchList,
        product,
        newProducts,
        favourites,
        ratingList,
      };
      setMainPageData(productData);
    }
  };

  return (
    <>
      <div className="grid grid-cols-12 pl-[100px] pr-[100px] my-10">
        {data && <BreadCrum data={data} />}
        <section id="product-slider" className="col-start-1 col-span-9 ">
          <div className="product-info grid grid-cols-2">
            <ProductDetailSlider data={data}/>
            <div className="infomations px-2 ">
              {data && <Infomations data={data} />}
            </div>
          </div>
        </section>

        <section className="col-start-10 col-span-3 border box-border rounded">
          {data && <ShowRoom data={data.branchList} />}
        </section>

        <section className="col-end-10 col-span-9 pt-4">
          {data && <Prize />}
          {data && <ProductSharing />}
          {data && <MultiTaps data={data} />}
          <div className="pt-10">
            <div className="pb-10">
              {data && (
                <ProductListSlider
                  title={"Sản Phẩm Tương Tự"}
                  data={data.newProducts}
                />
              )}
            </div>

            <div>
              {data && (
                <ProductListSlider
                  title={"Sản Phẩm Yêu Thích"}
                  data={data.favourites}
                />
              )}
            </div>
          </div>
        </section>
        {isLoading && <Loading />}
      </div>
    </>
  );
};

export default ProductDetailPage;
