import React from "react";
import { Link, useLocation } from "react-router-dom";

const TopTitle = (props) => {
  const location = useLocation();
  
  const renderBreadCrumb = () => {
    return (
      <nav>
        <ol className="flex items-center gap-2">
          <li>
            <Link className="font-medium" to="/admin">
              Trang chủ /
            </Link>
          </li>
          <li className="font-medium text-primary">{props.title}</li>
        </ol>
      </nav>
    )
  }

  return (
    <div className="top-title-page mb-6 flex flex-col gap-3 sm:flex-row sm:items-center sm:justify-between border-b pb-2">
      <h2 className="font-bold text-black dark:text-white">
        {props.title}
      </h2>
      {location.pathname !== '/admin' && renderBreadCrumb()}
    </div>
  );
};

export default TopTitle;
