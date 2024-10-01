import React from "react";
import { Link } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faChevronRight } from "@fortawesome/free-solid-svg-icons";

const NewsCard = ({ data }) => {
  const firstItem = data[0];
  const lastFourItems = data.slice(-4);
  //https://mediaelly.sgp1.digitaloceanspaces.com/uploads/2024/01/10155551/z5058129671691_b4fb32c47ed7f13c6daedb2a9ff9a98a.jpg
  return (
    <div className="news-card h-[730px]">
      <div className="uppercase text-center text-2xl first-line:font-bold border py-2">
        {data[0].typeName}
      </div>
      <div className="border box-border px-4 pt-3 flex flex-col">
        <ul className="">
          <li className="top-news h-[375px] w-full">
            <div className="h-full w-full">
              <Link
                to={"https://flowbite.com/"}
                className="relative block h-full w-full truncate "
              >
              <img src={data[0].image} className="h-full w-full "/> 
                <p className="absolute bottom-0 bg-black bg-opacity-75 w-full px-2 text-white truncate text-lg">
                  {firstItem.title}
                </p>
              </Link>
            </div>
          </li>
          {lastFourItems.map((item) => {
            return (
              <li className="w-full truncate border-b place-content-center py-2">
                <Link className="text-base w-full">{item.title}</Link>
              </li>
            );
          })}        
        </ul>
        <Link
            to={"https://www.google.com/"}
            className="underline text-center h-[60px] p-2"
          >
            Xem Tất Cả
            <FontAwesomeIcon className="px-2" icon={faChevronRight} />
          </Link>
      </div>
    </div>
  );
};

export default NewsCard;
