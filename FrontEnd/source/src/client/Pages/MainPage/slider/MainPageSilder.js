import React from "react";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import Slider from "react-slick";

const MainPageSilder = ({data}) => {
  const mainPageSilderPosition = data.filter(
    (data) => data.position === "MultiPage_Top"
  );
  const settings = {
    dots: true,
    infinite: true,
    slidesToShow: 1,
    slidesToScroll: 1,
    autoplay: true,
    autoplaySpeed: 2000,
    pauseOnHover: true
  };
  return (
    <div className="slider-container h-full w-full">
      <div className="slider-container">
        <Slider {...settings}>
          {mainPageSilderPosition && mainPageSilderPosition.map((item, index) => (
            <div key={index}>
              <img src={item.picture} />
            </div>
          ))}
        </Slider>
      </div>
    </div>
  );
};

export default MainPageSilder;
