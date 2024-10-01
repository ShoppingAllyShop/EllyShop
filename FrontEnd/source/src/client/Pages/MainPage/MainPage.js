import React, { useEffect, useState } from 'react'
import PauseOnHover from './slider/MainPageSilder'
import ProductListSlider from './Components/ProductListSlider';
import CategoryList from './Components/CategoryList';
import './MainPage.scss'
import Banner from './Banner/Banner';
import NewSection from './Components/NewSection';
import Jornal from '../../layout/Jornal';
import useAxiosBase from '../../../api/useAxiosBase';
import { useDispatch, useSelector } from 'react-redux';
import { STATUS_RESPONSE_HTTP_ENUM } from '../../../constants/enum';
import { RESPONSE_API_STATUS } from '../../../constants/common';
import { CLIENT_ENDPOINT } from '../../../constants/endpoint';
import Loading from '../../../components/Loading';
import { fakeData } from '../cookdata/mainPageData';
const MainPage = () => {
  const axiosBase = useAxiosBase();
  const dispatch = useDispatch();
  const [data, setData] = useState(null);
  const [isLoading, setIsLoading] = useState(false);
  //const { branchList,newList,newMediaList,prizeList } = data
  console.log('aaaaaaaaaa', data)
  const promotionData = fakeData.products.filter(
    (item) =>
    item.tagName === "Promotion"
  );
  const newProductData = fakeData.products.filter(
    (item) =>
    item.tagName === "New"
  );
  const bestSaleData = fakeData.products.filter(
    (item) =>
    item.tagName === "BestSaler"
  );
  const favoriteData = fakeData.products.filter(
    (item) =>
    item.tagName === "Favorite"
  );
  
  //Chay vo đây 1 lần, sau lần render đầu tiên
  useEffect(() => {
    fetchData();
    console.log('fetchData')
  }, []); 

  const fetchData = async () => {
    const endpoint = CLIENT_ENDPOINT.MAIN_PAGE_CONTENT;
    setIsLoading(true);
    const [mainPageResponse] = await Promise.all([axiosBase.get(endpoint)]);    
    console.log('mainPageResponse', mainPageResponse)
    if (
      mainPageResponse.status === STATUS_RESPONSE_HTTP_ENUM.OK &&
      mainPageResponse.data.status === RESPONSE_API_STATUS.SUCCESS
    ) {
      //dispatch store
      setIsLoading(false);
      setData(mainPageResponse.data.result.mainPageData);
      //dispatch(setData(mainPageResponse.data.result));
    }
  };

  return (
    <>
    <PauseOnHover/>
    <ProductListSlider data={promotionData}/>
    <ProductListSlider data={newProductData}/>
    <ProductListSlider data={bestSaleData}/>
    <ProductListSlider data={favoriteData}/>
    <CategoryList/>
    {data && <NewSection data={data}/>}
    {data && <Jornal data={data.newsMediaList}/>} 
    <Banner/>
    {isLoading && <Loading />}   
    </>
  )
}

export default MainPage