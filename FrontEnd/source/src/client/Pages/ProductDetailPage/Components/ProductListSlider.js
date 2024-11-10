import React from "react";
import ProductCard from "../../Common/ProductCard";
import {
  faChevronLeft,
  faChevronRight,
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import Slider from "react-slick";

const ProductListSlider = ({ data, title }) => {
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
  const settings = {
    infinite: true,
    slidesToShow: 3,
    slidesToScroll: 3,
    autoplay: true,
    autoplaySpeed: 5000,
    nextArrow: <SampleNextArrow />,
    prevArrow: <SamplePrevArrow />,
  };
  return (
    <div className="slider-container ">
      <div className="group/arrow">
      <div className="font-medium first-line:uppercase pb-2 px-4 ">
        <h2 className="text-xl">{title}</h2>
      </div>
        <Slider {...settings}>
          {data &&
            data.map((data, index) => (
              <ProductCard key={index} product={data} />
            ))}
        </Slider>
      </div>
    </div>
  );
};

export default ProductListSlider;
