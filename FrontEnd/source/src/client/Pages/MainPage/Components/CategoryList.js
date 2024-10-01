import React from "react";
import { Link } from "react-router-dom";

const ProductList = () => {
  const data = [{id:1, name: "túi xách", url:"aaaaaaaaa"}, {id:2, name: "Balo"}, {id:3, name: "Clutch"}]
  return (
    <section className="cate-list ">
      <div className="first-line:font-bold first-line:uppercase text-center text-2xl">
        Danh mục sản Phẩm
      </div>
      <div className="text-center">
        Cùng chiêm ngưỡng các danh mục sản phẩm tiêu biểu của ELLY
      </div>
      <div className="grid grid-cols-3 mx-10 ">
        <div className="Hand-Bag col-span-2 p-5">
          <div className="relative h-[380px] overflow-hidden group/category">
            <img src="https://elly.vn/wp-content/uploads/2020/12/921x380-TuiXach.jpg"
              className="group-hover/category:scale-105 duration-500 h-full w-full bg-center bg-cover"
            />
            <div className="absolute inset-0 bg-[#0000002b] opacity-50 "></div>
            <div className="absolute z-10 flex items-center justify-center h-full w-full px-6 text-center text-white left-0 top-0">
              <div>
                <Link
                  href="#"
                  className="uppercase bg-gray hover:bg-white border-2 hover:text-black text-gray-400 font-bold py-3 px-6 rounded-lg transition duration-300"
                >
                  Túi Xách
                </Link>
              </div>
            </div>
          </div>
        </div>
        <div className="Bag p-5">
        <div className="relative h-[380px] overflow-hidden group/category">
            <img src="https://elly.vn/wp-content/uploads/2020/12/450x380-Balo.jpg"
              className="group-hover/category:scale-105 duration-500 h-full w-full bg-center bg-cover"
            />
            <div className="absolute inset-0 bg-[#0000002b] opacity-50 "></div>
            <div className="absolute z-10 flex items-center justify-center h-full w-full px-6 text-center text-white left-0 top-0">
              <div>
                <Link
                  href="#"
                  className="uppercase bg-gray hover:bg-white border-2 hover:text-black text-gray-400 font-bold py-3 px-6 rounded-lg transition duration-300"
                >
                  Túi Xách
                </Link>
              </div>
            </div>
          </div>
        </div>
        <div className="Clutch p-5">
        <div className="relative h-[380px] overflow-hidden group/category">
            <img src="https://elly.vn/wp-content/uploads/2020/12/450x380-Clutch.jpg"
              className="group-hover/category:scale-105 duration-500 h-full w-full bg-center bg-cover"
            />
            <div className="absolute inset-0 bg-[#0000002b] opacity-50 "></div>
            <div className="absolute z-10 flex items-center justify-center h-full w-full px-6 text-center text-white left-0 top-0">
              <div>
                <Link
                  href="#"
                  className="uppercase bg-gray hover:bg-white border-2 hover:text-black text-gray-400 font-bold py-3 px-6 rounded-lg transition duration-300"
                >
                  Túi Xách
                </Link>
              </div>
            </div>
          </div>
        </div>
        <div className="Wallet p-5">
        <div className="relative h-[380px] overflow-hidden group/category">
            <img src="https://elly.vn/wp-content/uploads/2020/12/450x380-Vi.jpg"
              className="group-hover/category:scale-105 duration-500 h-full w-full bg-center bg-cover"
            />
            <div className="absolute inset-0 bg-[#0000002b] opacity-50 "></div>
            <div className="absolute z-10 flex items-center justify-center h-full w-full px-6 text-center text-white left-0 top-0">
              <div>
                <Link
                  href="#"
                  className="uppercase bg-gray hover:bg-white border-2 hover:text-black text-gray-400 font-bold py-3 px-6 rounded-lg transition duration-300"
                >
                  Túi Xách
                </Link>
              </div>
            </div>
          </div>
        </div>
        <div className="belt p-5">
        <div className="relative h-[380px] overflow-hidden group/category">
            <img src="https://elly.vn/wp-content/uploads/2020/12/450x380-DayLung.jpg"
              className="group-hover/category:scale-105 duration-500 h-full w-full bg-center bg-cover"
            />
            <div className="absolute inset-0 bg-[#0000002b] opacity-50 "></div>
            <div className="absolute z-10 flex items-center justify-center h-full w-full px-6 text-center text-white left-0 top-0">
              <div>
                <Link
                  href="#"
                  className="uppercase bg-gray hover:bg-white border-2 hover:text-black text-gray-400 font-bold py-3 px-6 rounded-lg transition duration-300"
                >
                  Túi Xách
                </Link>
              </div>
            </div>
          </div>
        </div>
        <div className="flip-flop col-span-2 p-5">
        <div className="relative h-[380px] overflow-hidden group/category">
            <img src="https://elly.vn/wp-content/uploads/2020/12/921x380-GiayDep.jpg"
              className="group-hover/category:scale-105 duration-500 h-full w-full bg-center bg-cover"
            />
            <div className="absolute inset-0 bg-[#0000002b] opacity-50 "></div>
            <div className="absolute z-10 flex items-center justify-center h-full w-full px-6 text-center text-white left-0 top-0">
              <div>
                <Link
                  href="#"
                  className="uppercase bg-gray hover:bg-white border-2 hover:text-black text-gray-400 font-bold py-3 px-6 rounded-lg transition duration-300"
                >
                  Túi Xách
                </Link>
              </div>
            </div>
          </div>
        </div>
        <div className="glasses p-5">
        <div className="relative h-[380px] overflow-hidden group/category">
            <img src="https://elly.vn/wp-content/uploads/2020/12/450x380-Kinh.jpg"
              className="group-hover/category:scale-105 duration-500 h-full w-full bg-center bg-cover"
            />
            <div className="absolute inset-0 bg-[#0000002b] opacity-50 "></div>
            <div className="absolute z-10 flex items-center justify-center h-full w-full px-6 text-center text-white left-0 top-0">
              <div>
                <Link
                  href="#"
                  className="uppercase bg-gray hover:bg-white border-2 hover:text-black text-gray-400 font-bold py-3 px-6 rounded-lg transition duration-300"
                >
                  Túi Xách
                </Link>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};

export default ProductList;
