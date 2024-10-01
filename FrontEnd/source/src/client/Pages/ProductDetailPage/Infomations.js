import React, { useState } from "react";
import HtmlContent from "../../../components/HtmlContent";
import { formatCurrency } from "../../../utils/numberUtil";

const Infomations = (props) => {
  const { productId, name, colors, sizes, price, discount, guarantees } =
    props.data;
  const [selectedColor, setSelectedColor] = useState("");
  const [selectedSize, setSelectedSize] = useState("");
  const isHasDiscount = discount != null;

  const handleClickColor = (name) => {
    console.log("name:", name);
    setSelectedColor(name);
  };

  const handleClickSize = (size) => {
    console.log("size:", size);
    setSelectedSize(size);
  };

  const renderChooseColor = () => {
    return (
      <div className="choose-color">
        <div>
          <p className="text-sm font-bold">Màu sắc : {selectedColor}</p>
        </div>
        <div className="pt-2 w-full h-full ">
          {colors &&
            colors.map((item) => (
              <button
                onClick={() => handleClickColor(item.name)}
                className="mx-1 border-2 text-center rounded bg-white w-[60px]"
              >
                {item.name}
              </button>
            ))}
        </div>
      </div>
    );
  };

  const renderChooseSize = () => {
    return (
      <div className="choose-size pt-2">
        <div>
          <p className="text-sm font-bold">Size : {selectedSize}</p>
        </div>
        <div className="pt-2 w-full h-full ">
          {sizes &&
            sizes.map((item) => (
              <button
                onClick={() => handleClickSize(item.size)}
                className="mx-1 border-2 text-center rounded bg-white w-[40px] hover:border-black "
              >
                {item.size}
              </button>
            ))}
        </div>
      </div>
    );
  };

  const renderGuarantees = () => {
    return (
      <ul className="featuredelly ">
        {guarantees &&
          guarantees.map((item) => (
            <li className="spcl flex pt-3">
              <i
                className={`${item.icon} border-2  rounded-full text-2xl text-center w-[50px] h-[50px] p-2 text-red-600`}
              />
              <div className="w-full px-2">
                <p className="uppercase font-semibold">{item.title}</p>
                <p className="text-sm font-thin">{item.contents}</p>
              </div>
            </li>
          ))}
      </ul>
    );
  };


  const renderPurchaseOrder = () => {
    return(
      <div className="purchase-order">
          <div className="pt-4">
            <button className="w-full bg-red-700 text-center rounded p-2">
              <p className="text-white uppercase text-2xl">Đặt Hàng Nhanh</p>
              <p className="text-white">Giao hàng nhanh toàn quốc miễn phí</p>
            </button>
          </div>
          <div className="pt-4">
            <button className="w-full border-2 text-center rounded p-2">
              <p className="uppercase">Thêm vào giỏ hàng</p>
            </button>
          </div>
          <div className="pt-4">
            <button className="w-full text-center p-2 border-2">
              <p className="uppercase">
                Xem trực tiếp tại <span className="font-bold">40</span> ShowRoom
              </p>
            </button>
          </div>
        </div>
    )
  }

  return (
    <div className="border box-border shadow-lg rounded">
      <div className="text-2xl font-semibold p-4">
        {name} - {productId}
      </div>

      <div className="rating-star"></div>
      <div className="price flex w-full px-2 bg-gray-300 text-lg">
        <div className="flex w-full text-xl py-3">
          <p
            className={`px-3 ${isHasDiscount ? "opacity-40 line-through" : ""}`}
          >
            {formatCurrency(price)}
            <span className="underline">đ</span>
          </p>
          {isHasDiscount && (
            <p className="text-red-800">
              {formatCurrency(discount)}
              <span className="underline">đ</span>
            </p>
          )}
        </div>
      </div>
      <div className="infomation p-4 ">
        <HtmlContent html={props.data.shortDescription} />
      </div>
      <div className="choose-option-order p-4 w-full bg-gray-300">
        {renderChooseColor()}
        {renderChooseSize()}
        {renderPurchaseOrder()}
      </div>
      <div className="product-fetured p-4">
        {renderGuarantees()}
      </div>
    </div>
  );
};

export default Infomations;
