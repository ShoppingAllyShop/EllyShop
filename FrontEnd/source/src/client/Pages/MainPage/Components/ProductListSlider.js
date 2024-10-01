import React, { useState } from "react";
import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import { Link } from "react-router-dom";
import ProductCard from "../../Common/ProductCard";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faChevronLeft,
  faChevronRight,
} from "@fortawesome/free-solid-svg-icons";
function SampleNextArrow(props) {
  const { onClick } = props;
  return (
    <button
      className="text-black group-hover/arrow:-translate-x-6 opacity-0 group-hover/arrow:opacity-100 transition duration-500 absolute right-full top-1/2 size-10 -translate-y-10 font-semibold rounded-full border border-black hover:text-white hover:bg-red-600 hover:border-transparent focus:outline-none "
      onClick={onClick}
    >
      <FontAwesomeIcon className="px-2 text-1xl" icon={faChevronLeft} />
    </button>
  );
}

function SamplePrevArrow(props) {
  const { onClick } = props;
  return (
    <button
      className="text-black group-hover/arrow:translate-x-6 opacity-0 group-hover/arrow:opacity-100 transition duration-500 absolute left-full top-1/2 size-10 -translate-y-10 font-semibold rounded-full border border-black hover:text-white hover:bg-red-600 hover:border-transparent focus:outline-none"
      onClick={onClick}
    >
      <FontAwesomeIcon className="px-2 text-1xl" icon={faChevronRight} />
    </button>
  );
}
const ProductListSlider = ({ data }) => {
  const settings = {
    infinite: true,
    slidesToShow: 4,
    slidesToScroll: 4,
    autoplay: true,
    autoplaySpeed: 5000,
    nextArrow: <SampleNextArrow />,
    prevArrow: <SamplePrevArrow />,
  };

  return (
    <>
    <section className="product-slider h-[675px] relative">
      <div className="font-medium first-line:uppercase text-center pb-2">
        <h2 className="text-4xl">{data[0].tagDescription}</h2>
      </div>
      <p className="text-center pb-4">{data[0].tagTitle}</p>
      <div className="group/arrow">
        <div className="slider-container mx-20 px-10">
          <Slider {...settings} className="">
            {data &&
              data.map((product, index) => (
                <ProductCard key={index} product={product} />
              ))}
          </Slider>
        </div>
        <div className="text-center pb-7 pt-4">
          <Link to={"https://www.google.com/"} className="underline">
            Xem Tất Cả <FontAwesomeIcon className="px-2 text-1xl" icon={faChevronRight} />
          </Link>
        </div>
      </div>
    </section>
    </>

  );
};

export default ProductListSlider;
