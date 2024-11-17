import { faPhoneVolume } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import React from "react";
import { Link } from "react-router-dom";

const ProductSharing = () => {
  return (
    <div className="product_sharing">
      <ul className="fastcontact ">
        <li className="mli-phone text-center pt-3  ">
          <div className="flex justify-center">
            <FontAwesomeIcon icon={faPhoneVolume} className="text-2xl px-4 fa-shake" />
            <p className="uppercase"> GỌI MUA HÀNG </p>
          </div>
          <Link className="text-red-600 ">0906.636.000 - 0966.353.000</Link>
        </li>
      </ul>
    </div>
  );
};

export default ProductSharing;
