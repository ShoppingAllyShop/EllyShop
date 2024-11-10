import React, { useEffect, useRef, useState } from "react";
import Slider from "react-slick";
import ZoomImage from "./ZoomImage";
import {
  faChevronLeft,
  faChevronRight,
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useSelector } from "react-redux";

const ProductDetailSlider = ({ data }) => {
  let colorBigImageCount = 0; // Biến để đếm số lượng phần tử
  let colorSmallImageCount = 0;
  const selectedColor = useSelector((state) => state?.selectedColor?.data);
  const sourceSelectedColorImage = data?.product?.productImages?.find(
    (image) => image?.colorId === selectedColor?.id
  )?.picture;
  const productImageDataSource = data?.product?.productImages
  const [nav1, setNav1] = useState(null);
  const [nav2, setNav2] = useState(null);
  const slider1 = useRef(null);
  const slider2 = useRef(null);
  const settings1 = {
    asNavFor: nav2,
    ref: slider1,
    slidesToShow: 1,
    arrows: true,
    infinite: true,
    nextArrow: <SampleNextArrow />,
    prevArrow: <SamplePrevArrow />,
    fade: true,
  };

  const settings2 = {
    asNavFor: nav1,
    ref: slider2,
    slidesToShow: 4,
    arrows: true,
    swipeToSlide: true,
    focusOnSelect: true,
    infinite: true,
    nextArrow: <SampleNextArrow />,
    prevArrow: <SamplePrevArrow />,
  };


  useEffect(() => {
    // Chuyển về slide đầu tiên khi chon mau
    console.log("huhu");
    if (slider1.current && slider1.current) {
      slider1.current.slickGoTo(0);  
      slider2.current.slickGoTo(0);
    }
  }, [sourceSelectedColorImage]);

  useEffect(() => {
    setNav1(slider1.current);
    setNav2(slider2.current);
  }, []);

  function SamplePrevArrow(props) {    
    const { onClick } = props;
    return (
      <button
        className="z-50 text-black translate-x-16 group-hovesr/arrow:translate-x-12 opacity-0 group-hover/arrow:opacity-100 transition duration-500 absolute right-full top-1/2 size-10 -translate-y-10  font-semibold border-black hover:text-red-700  hover:border-transparent focus:outline-none "
        onClick={onClick}
      >
        <FontAwesomeIcon className="px-2 text-2xl" icon={faChevronLeft} />
      </button>
    );
  }

  function SampleNextArrow(props) {
    const { onClick } = props;
    return (
      <button
        className="z-50 text-black -translate-x-16 group-hover/arrow:-translate-x-14  opacity-0 group-hover/arrow:opacity-100 transition duration-500 absolute left-full top-1/2 size-10 -translate-y-10 font-semibold  border-black hover:text-red-700 hover:border-transparent focus:outline-none"
        onClick={onClick}
      >
        <FontAwesomeIcon className="px-2 text-2xl" icon={faChevronRight} />
      </button>
    );
  }

  return (
    <div className="slider w-full ">
      <div className="slider-container w-full">
        <div className="above-slider group/arrow relative">
          <Slider {...settings1}>
            {productImageDataSource && productImageDataSource.map((item, index) => {
                if (colorBigImageCount > 0 && item.type === "Color")
                  return null;
                if (item.type === "Color") colorBigImageCount += 1;

                return (
                  <div key={`main-${index}`} className="w-full relative">
                    <ZoomImage src={index === 0 ? sourceSelectedColorImage : item.picture} alt="" zoomLevel={300} />
                  </div>
                );
              })}
          </Slider>
        </div>
        <div>
          <div className="below-slider group/arrow pt-3">
            <Slider {...settings2}>
              {data &&
                data.product.productImages.map((item, index) => {
                  if (colorSmallImageCount > 0 && item.type === "Color")
                    return null;
                  if (item.type === "Color") colorSmallImageCount += 1;

                  return (
                    <div
                      className="px-2"
                      key={`thumb-${index}`}
                    >
                      <div className={`relative small-image-block px-3 hover:border-gray-500 border border-transparent overflow-hidden  hover:border-opacity-20 cursor-grab `} >
                        <img
                          className="mx-auto opacity-50 hover:-translate-y-2 hover:opacity-100 duration-700 "
                          alt={`Thumbnail ${index + 1} `}
                          src={index === 0 ? sourceSelectedColorImage : item.picture}
                        />
                      </div>
                    </div>
                  );
                })}
            </Slider>
          </div>
        </div>
      </div>
    </div>
  );
};

export default ProductDetailSlider;
