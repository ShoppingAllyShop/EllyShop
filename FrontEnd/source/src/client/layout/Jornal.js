import React from "react";
import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";

const Jornal = (props) => {
  const images = props.data;
  console.log("images",images)
  const settings = {
    className: "center",
    infinite: true,
    slidesToShow: 3,
    speed: 500,
    rows: 2,
    slidesPerRow: 3,
  };
  return (
      <section className="pl-[100px] pr-[100px] my-10">
        <h2 className="text-center p-2 border box-border uppercase text-3xl">
          BÁO CHÍ NÓI VỀ ELLY
        </h2>
        <div className=" slider-container border box-border px-4">
          <div >
            <Slider {...settings}>
              {images.map((item, index) => (
                <div className="py-2 px-2 h-[92px]">
                  <div
                    key={index}
                    className="slide border-2 shadow-lg content-center h-[80px] p-2 rounded-md"
                  >
                    <img
                      src={item.image}
                      alt={item.alt}
                      className="slide-image object-cover mx-auto "
                    />
                  </div>
                </div>
              ))}
            </Slider>
          </div>
        </div>
      </section>
  );
};

export default Jornal;
