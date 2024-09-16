import React from "react";
import SelectListItem from "./SelectListItem";
import { useSelector } from "react-redux";
import CategoryFormModal from "./CategoryFormModal";

const CategorySelectList = () => {
  const data = useSelector((state) => state.adminCategory.data);

  const selectedCate = useSelector(
    (state) => state.adminCategory.selectedCategory
  );

  const createEditModal = useSelector(
    (state) => state.adminCategory.createEditModal
  );

  const getCateDataByLevel = (level, parentId) => {
    if (level === 1) return data.filter((cate) => cate.categoryLevel === level);

    return data.filter(
      (cate) =>
        cate.categoryLevel === level &&
        cate.parentId?.toLowerCase() === parentId?.toLowerCase()
    );
  };

  const isSelectedByLevel = (level) => {
    return (
      selectedCate.filter((cate) => cate.categoryLevel === level).length > 0
    );
  };

  const renderCategorySelection = () => {
    const selectedCateLevelOne = selectedCate.find(
      (cate) => cate.categoryLevel === 1
    );
    const selectedCateLevelTwo = selectedCate.find(
      (cate) => cate.categoryLevel === 2
    );
    const selectedCateLevelThree = selectedCate.find(
      (cate) => cate.categoryLevel === 3
    );

    return (
      <>
        {/* category Level 1 */}
        <SelectListItem
          size={5}
          data={getCateDataByLevel(1)}
          level={1}
          isShowStepIcon={selectedCate.length > 0}
          selectedCategory={selectedCateLevelOne}
          isHaveChildren={data.some(
            (item) =>
              item.parentId?.toLowerCase() ===
              selectedCateLevelOne?.id.toLowerCase()
          )}
        />
        {/* category Level 2 */}
        {isSelectedByLevel(1) && (
          <SelectListItem
            data={getCateDataByLevel(2, selectedCateLevelOne.id)}
            size={5}
            level={2}
            isShowStepIcon={selectedCateLevelTwo?.id}
            selectedCategory={selectedCateLevelTwo}
            isHaveChildren={data.some(
              (item) =>
                item.parentId?.toLowerCase() ===
                selectedCateLevelTwo?.id.toLowerCase()
            )}
          />
        )}
        {/* category Level 3 */}
        {isSelectedByLevel(1) && isSelectedByLevel(2) && (
          <SelectListItem
            data={getCateDataByLevel(3, selectedCateLevelTwo.id)}
            size={5}
            level={3}
            selectedCategory={selectedCateLevelThree}
            isHaveChildren={data.some(
              (item) =>
                item.parentId?.toLowerCase() ===
                selectedCateLevelThree?.id.toLowerCase()
            )}
          />
        )}
        {createEditModal.isDisplay && (
          <CategoryFormModal
            level={createEditModal.level}
            categoryData={selectedCate.find(
              (cate) => cate.categoryLevel === createEditModal.level
            )}
            modalType={createEditModal.type}
          />
        )}
      </>
    );
  };

  return (
    <div className="category-selection grid grid-cols-3">
      {renderCategorySelection()}
    </div>
  );
};

export default CategorySelectList;
