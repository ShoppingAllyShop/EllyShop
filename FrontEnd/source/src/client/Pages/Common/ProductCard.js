import React from "react";
import { Link } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCartShopping, faEye } from "@fortawesome/free-solid-svg-icons";
import { formatCurrency } from "../../../utils/numberUtil";
const ProductCard = ({ product }) => {
  
  const isHasDiscount = product.discount != null;

  return (
    <div className="product-card slide relative border mx-5">
      {isHasDiscount && (
        <div className="border-2 bg-red-500 py-2 size-12 absolute translate-x-3 translate-y-3 text-center text-white rounded-full z-10">
          -{product.percentDiscount}%
        </div>
      )}
      <div className="relative group/img overflow-hidden">
        <img
          src={product.imagePicture}
          className="h-[404px] slide-image hinh-1 inset-0 w-full object-cover transition-opacity duration-300 ease-in-out hover:opacity-0"
        />
        <img
          src="https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2024/04/12160025/Tui-xach-nu-thoi-trang-ELLY-EL332-18-600x600.jpg"
          className="slide-image absolute hinh-2 inset-0 h-[404px] left-0 top-0 object-cover transition-opacity duration-300 ease-in-out opacity-0 hover:opacity-100 "
        />
        <div
          className="absolute opacity-0 bottom-0 text-center w-full 
        grid grid-cols-2 group-hover/img:opacity-100 translate-y-6 group-hover/img:translate-y-0 transition duration-500"
        >
          <div className="bg-[#c0392b] text-white border-r border-white">
            <FontAwesomeIcon icon={faCartShopping} />
            <Link className="">Thêm giỏ hàng </Link>
          </div>
          <div className="bg-[#c0392b] text-white border-l border-white">
            <FontAwesomeIcon icon={faEye} />
            <Link className="">Xem Chi Tiết</Link>
          </div>
        </div>
      </div>
      <div>
        <p className="text-center pt-2">
          {product.productName} - {product.productId}
        </p>
        <div className="flex w-full justify-center p-3">
          <p
            className={`px-3 ${isHasDiscount ? "opacity-40 line-through" : ""}`}
          >
            {formatCurrency(product.price)}
            <span className="underline">đ</span>
          </p>
          {isHasDiscount && (
            <p className="text-red-800">
              {formatCurrency(product.discount)}
              <span className="underline">đ</span>
            </p>
          )}
        </div>
      </div>
    </div>
  );
};

export default ProductCard;
