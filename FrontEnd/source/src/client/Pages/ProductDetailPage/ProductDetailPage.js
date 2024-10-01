import React, { useState, useEffect, useRef } from "react";
import BreadCrum from "./BreadCrum";
import Slider from "react-slick";
import ShowRoom from "./ShowRoom";
import Infomations from "./Infomations";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faChevronLeft,
  faChevronRight,
} from "@fortawesome/free-solid-svg-icons";
import { data } from "../cookdata/productDetailData";
import Prize from "./Prize";
import ProductSharing from "./ProductSharing";
import MultiTaps from "./MultiTaps";
const ProductDetailPage = () => {

  function SampleNextArrow(props) {
    const { onClick } = props;
    return (
      <button
        className=" text-black group-hover/arrow:translate-x-6 opacity-0 group-hover/arrow:opacity-100 transition duration-500 absolute right-full top-1/2 size-10 -translate-y-10  font-semibold border-black hover:text-red-700  hover:border-transparent focus:outline-none "
        onClick={onClick}
      >
        <FontAwesomeIcon className="px-2 text-2xl" icon={faChevronLeft} />
      </button>
    );
  }

  function SamplePrevArrow(props) {
    const { onClick } = props;
    return (
      <button
        className="text-black group-hover/arrow:-translate-x-6 opacity-0 group-hover/arrow:opacity-100 transition duration-500 absolute left-full top-1/2 size-10 -translate-y-10 font-semibold  border-black hover:text-red-700 hover:border-transparent focus:outline-none"
        onClick={onClick}
      >
        <FontAwesomeIcon className="px-2 text-2xl" icon={faChevronRight} />
      </button>
    );
  }

  const [nav1, setNav1] = useState(null);
  const [nav2, setNav2] = useState(null);
  const slider1 = useRef(null);
  const slider2 = useRef(null);

  useEffect(() => {
    setNav1(slider1.current);
    setNav2(slider2.current);
  }, []);

  const settings1 = {
    asNavFor: nav2,
    ref: slider1,
    slidesToShow: 1,
    arrows: true,
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
    nextArrow: <SampleNextArrow />,
    prevArrow: <SamplePrevArrow />,
  };

  const src = [
    "https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2018/09/09234719/tui-clutch-nu-thoi-trang-cao-cap-elly-ech17-14-2.jpg",
    "https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2018/09/07233319/tui-clutch-nu-thoi-trang-cao-cap-elly-ech17-3-2.jpg",
    "https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2018/09/10224530/tui-clutch-nu-thoi-trang-cao-cap-elly-ech17-3-4.jpg",
    "https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2018/09/08233626/tui-clutch-nu-thoi-trang-cao-cap-elly-ech17-2-4.jpg",
    "https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2018/09/06095623/tui-clutch-nu-thoi-trang-cao-cap-elly-ech17-8-1.jpg",
    "https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2018/09/06095630/tui-clutch-nu-thoi-trang-cao-cap-elly-ech17-9-1.jpg",
    "https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2018/09/30220947/ECH17H-1.jpg",
    "https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2018/09/09234645/tui-clutch-nu-thoi-trang-cao-cap-elly-ech17-8-3.jpg"
  ];

  const ZoomImage = () => {
    const [stateNe, setStateNe] = useState({
      backgroundImage: `url(https://images.unsplash.com/photo-1444065381814-865dc9da92c0)`,
      backgroundPosition: "0% 0%"
    });
    const images = [
      "https://images.unsplash.com/photo-1444065381814-865dc9da92c0",
      // "https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2022/05/11230053/el204-2-300x300.jpg",
      // "https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2018/01/13154000/tui-xach-nu-cao-cap-da-that-ELLY-ET63-16-2-300x300.jpg",
      // "https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2022/10/12142449/giay-nu-cao-cap-da-that-elly-egt194-19-2-600x600.jpg",
      // "https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2021/09/27203909/clutch-nu-cao-cap-da-that-elly-ec60-17-300x300.jpg",
      // "https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2022/05/07124218/ech62-1-300x300.jpg",
    ];
    const handleMouseMove = (e) => {
      const { left, top, width, height } = e.target.getBoundingClientRect();
      const x = ((e.pageX - left) / width) * 100;
      const y = ((e.pageY - top) / height) * 100;
      setStateNe({
        backgroundImage: `https://images.unsplash.com/photo-1444065381814-865dc9da92c0`,
        backgroundPosition: `${x}% ${y}%`,
      });
    };
    return (
      <figure
      onMouseMove={handleMouseMove}
      style={stateNe}
      className="group bg-no-repeat"
    >
      <img
        className="group-hover:opacity-0 block h-[600px]"
        alt={`Main Image`}
        src={src}
      />
    </figure>
    );
  };

  return (
    <>
      <div className="grid grid-cols-12 pl-[100px] pr-[100px] my-10">
        <section className="col-start-1 col-span-9 ">
          <BreadCrum data={data} />
          <div className="product-info grid grid-cols-2">
            <div className="slider w-full ">
              <div className="slider-container w-full">                        
                <div className="above-slider group/arrow">
                  <Slider {...settings1}>
                    {src.map((src, index) => (
                      <div key={`main-${index}`} className="aaa ">
                        <img
                          className="mx-auto  duration-700 "
                          alt={`Thumbnail ${index + 1} `}
                          src={src}
                        />
                        {/* <ZoomImage /> */}
                      </div>
                    ))}
                  </Slider>
                </div>
                <div className="below-slider group/arrow pt-3">
                  <Slider {...settings2}>
                    {src.map((src, index) => (
                      <div className="px-2 ">
                        <div
                          key={`thumb-${index}`}
                          className=" relative px-3 hover:border-gray-500 border border-transparent overflow-hidden hover:border-opacity-20"
                        >
                          <img
                            className="mx-auto  opacity-50  hover:-translate-y-2 hover:opacity-100 duration-700 "
                            alt={`Thumbnail ${index + 1} `}
                            src={src}
                          />
                        </div>
                      </div>
                    ))}
                  </Slider>
                </div>
              </div>
            </div>

            <div className="infomations px-2 ">
              <Infomations data={data} />
            </div>
          </div>
        </section>

        <section className="col-start-10 col-span-3 border box-border rounded">
          <ShowRoom data={data}/>
        </section>

        <section className="w-[1280px] p-2">
          <Prize />
          <ProductSharing />
          <MultiTaps data={data}/>
        </section>
      </div>
    </>
  );
};

export default ProductDetailPage;
