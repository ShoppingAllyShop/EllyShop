import React from 'react'
import { Link } from 'react-router-dom'

const Prize = () => {
  return (
    <div className="grid grid-cols-2 gap-4">
    <div className="border box-border rounded col-span-2 border-black p-4">
      <Link className=" ">
        <img src="images/logo-giaithuong.png" alt="" />
        <p className="text-center text-[#c0392b]">
          TOP 10 THƯƠNG HIỆU NỔI TIẾNG VIỆT NAM 2023
          <br />
          SẢN PHẨM DỊCH VỤ TỐT NHẤT VÌ NGƯỜI TIÊU DÙNG
        </p>
      </Link>
    </div>
    <div className="border box-border rounded border-black p-4">
      <Link className=" ">
        <img
          src="/wp-content/themes/ml/img/product/cup_spcl.png"
          alt=""
        />
        <p className="text-center text-[#c0392b]">
          TOP 10 THƯƠNG HIỆU UY TÍN
          <br />
          SẢN PHẨM CHẤT LƯỢNG - DỊCH VỤ TIN DÙNG
        </p>
      </Link>
    </div>
    <div className="border box-border rounded border-black p-2">
      <Link className=" ">
        <img
          src="/wp-content/themes/ml/img/product/cup_top1003.png"
          alt=""
        />
        <p className="text-center text-[#c0392b]">
          TOP 100
          <br />
          THƯƠNG HIỆU, SẢN PHẨM, DỊCH VỤ
          <br />
          HÀNG ĐẦU VIỆT NAM
        </p>
      </Link>
    </div>
    <div className="border box-border rounded border-black p-2">
      <Link className=" ">
        <img
          src="/wp-content/themes/ml/img/product/cup_winner2.png"
          alt=""
        />
        <p className="text-center text-[#c0392b]">
          THƯƠNG HIỆU CHẤT LƯỢNG CHÂU Á
          <br />
          ASIA QUALITY BRANDS AWARD
        </p>
      </Link>
    </div>
    <div className="border box-border rounded border-black p-2">
      <Link className=" ">
        <img
          src="/wp-content/themes/ml/img/product/cup_top100dd.png"
          alt=""
        />
        <p className="text-center text-[#c0392b] uppercase">
          TOP 100
          <br />
          THƯƠNG HIỆU DẪN ĐẦU VIỆT NAM
        </p>
      </Link>
    </div>
  </div>
  )
}

export default Prize