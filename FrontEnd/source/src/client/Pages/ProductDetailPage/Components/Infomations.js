import React, { useEffect, useState } from "react";
import HtmlContent from "../../../../components/HtmlContent";
import { formatCurrency } from "../../../../utils/numberUtil";
import { setSelectedColor } from "../../../../redux/slices/client/selectedColorSlice";
import { useDispatch, useSelector } from "react-redux";

const Infomations = ({ data }) => {
  console.log("dataa", data);
  const { id, name, price, discount, shortDescription } = data.product;
  const catelog = data.product.category;
  const colors = data.product.productDetail.map((item) => item.color);
  const uniqueColorsById = [
    ...new Map(colors.map((item) => [item.id, item])).values(),
  ].sort((a, b) => a.name.localeCompare(b.name));
  const sizes = data.product.productDetail.map((item) => item.size);
  const uniqueSizesById = [
    ...new Map(sizes.map((item) => [item?.id, item])).values(),
  ].sort((a, b) => a?.size1.localeCompare(b?.size1));
  const guarantees = data.guaranteeList;
  const [selectedSize, setSelectedSize] = useState("");
  const [activeButtonId, setActiveButtonId] = useState(null);
  const isHasDiscount = discount != null;
  const dispatch = useDispatch();
  const selectedColor = useSelector((state) => state?.selectedColor?.data);

  useEffect(() => {
    const defaultColor = data.product.productImages.find(
      (image) => image?.defaultColor === true
    );
    console.log("123", uniqueColorsById);

    const infoColor = uniqueColorsById.find(
      (color) => color.id === defaultColor.colorId
    );  
    setActiveButtonId(defaultColor.colorId);

    dispatch(setSelectedColor({ id: defaultColor.colorId, name: infoColor.name}));
  }, []);

  const handleClickColor = (item) => {
    setActiveButtonId(item.id);
    dispatch(setSelectedColor(item));
  };

  const handleClickSize = (size) => {
    setSelectedSize(size);
  };

  const renderChooseColor = () => {
    return (
      <div className="choose-color">
        <div>
          <p className="text-sm font-bold">Màu sắc : {selectedColor.name}</p>
        </div>
        <div className="pt-2 w-full h-full">
          {uniqueColorsById &&
            uniqueColorsById.map((item) => {
              console.log("asdasd", item);
              return (
                <button
                  onClick={() => handleClickColor(item)}
                  className={`mx-1 p-1 border-2 text-center rounded-md bg-white w-[60px]
                            ${
                              activeButtonId === item.id ? "border-black" : ""
                            } `}
                  key={item.id}
                >
                  {item.name}
                </button>
              );
            })}
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
          {uniqueSizesById &&
            uniqueSizesById.map((item) => (
              <button
                onClick={() => handleClickSize(item.size1)}
                className="mx-1 border-2 text-center rounded bg-white w-[40px] hover:border-black "
                key={item.id}
              >
                {item.size1}
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
            <li className="spcl flex pt-3" key={item.id}>
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
    return (
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
    );
  };

  return (
    <div className="border box-border shadow-lg rounded">
      <div className="text-2xl font-semibold p-4">
        {name} - {id}
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
        <HtmlContent html={shortDescription} />
      </div>
      <div className="choose-option-order p-4 w-full bg-gray-300">
        {renderChooseColor()}
        {catelog.parentId === "abf5840d-a58a-4064-a961-de7b318c6799" &&
          renderChooseSize()}
        {renderPurchaseOrder()}
      </div>
      <div className="product-fetured p-4">{renderGuarantees()}</div>
    </div>
  );
};

export default Infomations;
