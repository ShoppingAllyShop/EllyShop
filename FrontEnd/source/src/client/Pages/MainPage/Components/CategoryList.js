import React from "react";
import { useSelector } from "react-redux";
import { Link } from "react-router-dom";

const CategoryList = ({ data }) => {

  const titles = [
    "Túi Xách",
    "Balo",
    "Clutch",
    "Ví Da",
    "Quần Jeans",
    "Giày Dép",
    "Kính Mắt",
  ];

  const pictureByPosition = data.filter(
    (data) => data.position === "MainPage_Bot"
  );
  return (
    <section className="cate-list ">
      <div className="first-line:font-bold first-line:uppercase text-center text-2xl">
        Danh mục sản Phẩm
      </div>
      <div className="text-center">
        Cùng chiêm ngưỡng các danh mục sản phẩm tiêu biểu của ELLY
      </div>
      <div className="grid grid-cols-3 gap-4 p-20">
        {pictureByPosition &&
          pictureByPosition.map((item, index) => {
            const span = item.span || (index === 0 || index === 5 ? 2 : 1);
            return (
              <div key={index}
                className={`relative h-[380px] overflow-hidden group/category col-span-${span}`}
              >
                <img
                  src={item.picture}
                  className="group-hover/category:scale-105 duration-500 h-full w-full bg-center bg-cover"
                />
                <div className="absolute inset-0 bg-[#0000002b] opacity-50 "></div>
                <div className="absolute z-10 flex items-center justify-center h-full w-full px-6 text-center text-white left-0 top-0">
                  <div>
                    <Link
                      href="#"
                      className="uppercase bg-gray hover:bg-white border-2 hover:text-black text-gray-400 font-bold py-3 px-6 rounded-lg transition duration-300"
                    >
                      {titles[index]}
                    </Link>
                  </div>
                </div>
              </div>
            );
          })}
      </div>
    </section>
  );
};

export default CategoryList;
