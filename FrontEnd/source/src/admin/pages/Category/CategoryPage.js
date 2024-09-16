import React from "react";
import TopTitle from "../../common/components/TopTitle";
import CategorySelectList from "./components/CategorySelectList";
import { cateData } from "./data";

const CategoryPage = () => {  
  return (
    <div className="mx-auto max-w-screen-2xl p-4 md:p-6 2xl:p-10">
      <TopTitle title="Danh mục sản phẩm" />
      <CategorySelectList data={cateData}/>
    </div>
  );
};

export default CategoryPage;
