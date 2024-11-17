import React, { useEffect, useState } from 'react'
import ProductListSlider from './Components/ProductListSlider';
import CategoryList from './Components/CategoryList';
import './MainPage.scss'
import Banner from './Banner/Banner';
import NewSection from './Components/NewSection';
import useAxiosBase from '../../../api/useAxiosBase';
import { useDispatch, useSelector } from 'react-redux';
import { STATUS_RESPONSE_HTTP_ENUM } from '../../../constants/enum';
import { RESPONSE_API_STATUS } from '../../../constants/common';
import { CLIENT_ENDPOINT } from '../../../constants/endpoint';
import Loading from '../../../components/Loading';
import { setData } from '../../../redux/slices/client/layoutSlice';
import MainPageSilder from './slider/MainPageSilder';

const MainPage = () => {
  const axiosBase = useAxiosBase();
  const dispatch = useDispatch();
  const [data, setMainPageData] = useState(null);
  const [isLoading, setIsLoading] = useState(false);
  //Chay vo đây 1 lần, sau lần render đầu tiên
  useEffect(() => {
    fetchData();
    window.scrollTo(0, 0);
  }, []); 

  const fetchData = async () => {
    const contentMainPageEndpoint = CLIENT_ENDPOINT.MAIN_PAGE_CONTENT;
    const catalogEndpoint = CLIENT_ENDPOINT.CATALOG_CONTENT;
    setIsLoading(true);
    const [contentMainPageResponse, catalogResponse] = await Promise.all(
        [axiosBase.get(contentMainPageEndpoint),
        axiosBase.get(catalogEndpoint)]
      );
    if (
      contentMainPageResponse.status === STATUS_RESPONSE_HTTP_ENUM.OK &&
      contentMainPageResponse.data.status === RESPONSE_API_STATUS.SUCCESS &&
      catalogResponse.status === STATUS_RESPONSE_HTTP_ENUM.OK &&
      catalogResponse.data.status === RESPONSE_API_STATUS.SUCCESS
    ) {
      //dispatch store
      setIsLoading(false);

       //handle mainpage data
      const {productList} = catalogResponse.data.result
      const {newsList,newsMediaList,branchList,prizeList,silderList} = contentMainPageResponse.data.result.mainPageData
      var mainpagedata = {productList,newsList,newsMediaList,branchList,prizeList,silderList}
      setMainPageData(mainpagedata);

      //handle layout data
      const {headerData, footerData} = contentMainPageResponse.data.result.layoutData
      const {categoryList, collectionsList, } = catalogResponse.data.result
      var layoutdata = {categoryList, collectionsList,headerData,footerData}
      dispatch(setData(layoutdata));
    }
  }; 

  return (
    <>    
      {data && <MainPageSilder data={data.silderList}/>}
      <div className='mx-20 px-10 pt-10'>
        {data && <ProductListSlider itemShowNumber={4} data={data.productList.promotions}/>}
      </div> 
      <div className='mx-20 px-10'>  
        {data && <ProductListSlider itemShowNumber={4} data={data.productList.newProducts}/>}
      </div>
      <div className='mx-20 px-10'>
        {data && <ProductListSlider itemShowNumber={4} data={data.productList.bestSellers}/>}
      </div>
      <div className='mx-20 px-10'>
        {data && <ProductListSlider itemShowNumber={4} data={data.productList.favourites}/>}
      </div>
      {data && <CategoryList data={data.silderList}/>}
      {data && <NewSection data={data}/>}
      <Banner/>
      {isLoading && <Loading />}   
    </>
  )
}

export default MainPage