import React, { useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import {
  setCreateCateButton,
  createNewCate,
} from "../../../../redux/slices/admin/categorySlice";
import useAxiosAuth from "../../../../api/useAxiosAuth";
import { RESPONSE_API_STATUS } from "../../../../constants/common";


const CategoryFormModal = ({ level, categoryData, modalType }) => {
  const dispatch = useDispatch();
  const auth = useAxiosAuth()
  const createEditModal = useSelector(
    (state) => state.adminCategory.createEditModal
  );
  const selectedCategory = useSelector(
    (state) => state.adminCategory.selectedCategory
  );
  const [formData, setFormData] = useState({
    categoryName: "",
  });

  const handleCloseModal = async () => {
    dispatch(setCreateCateButton({ isDisplay: false }));
    auth.get('user')    
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    console.log('dumamay')
    let parentId = null;
    switch (level) {
      case 2:
        parentId = selectedCategory[0].id
        break;
      case 3:
        parentId = selectedCategory[1].id
        break;
      default:
        break;
    }
    
    let newCate = {
      id: crypto.randomUUID(),
      name: formData.categoryName,
      categoryLevel: level,
      parentId: parentId
    };
    console.log('newCate',newCate)

    //Call login api
    const endpoint = "/admin/category/add";
    const [addResponse] = await Promise.all([
      auth.post(endpoint, newCate),
    ]);

    console.log('addResponse',addResponse)
    if (addResponse.data.status === RESPONSE_API_STATUS.SUCCESS) {
      
       //add to store
      dispatch(createNewCate(newCate));      
    }
   
  };

  return (
    <>
      <div
        tabIndex={-1}
        className="fixed inset-0 flex items-center justify-center z-50 bg-opacity-75 bg-gray-300"
      >
        <div className="m-auto">
          {/* Modal content */}

          <div className="relative bg-white rounded-lg shadow dark:bg-gray-700 w-[450px]">
            {/* Modal header */}
            <div className="flex items-center justify-between p-4 md:p-5 border-b rounded-t dark:border-gray-600">
              <h3 className="text-lg font-semibold text-gray-900 dark:text-white">
                {modalType === 'add' ? `Tạo danh mục cấp ${createEditModal.level}` : `Chỉnh sửa danh mục ${createEditModal.level}`}
              </h3>
              <button
                type="button"
                className="text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm w-8 h-8 ms-auto inline-flex justify-center items-center dark:hover:bg-gray-600 dark:hover:text-white"
                onClick={handleCloseModal}
              >
                <svg
                  className="w-3 h-3"
                  xmlns="http://www.w3.org/2000/svg"
                  fill="none"
                  viewBox="0 0 14 14"
                >
                  <path
                    stroke="currentColor"
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    strokeWidth={2}
                    d="m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6"
                  />
                </svg>
                <span className="sr-only">Close modal</span>
              </button>
            </div>
            {/* Modal body */}
            <form className="p-4 md:p-5" onSubmit={handleSubmit}>
              <div className="grid gap-4 mb-4 grid-cols-2">
                <div className="col-span-2">
                  <label
                    htmlFor="name"
                    className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
                  >
                    Tên
                  </label>
                  <input
                    type="text"
                    name="categoryName"
                    id="name"
                    className="bg-gray-50 border 
                    border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 
                    focus:border-primary-600 block w-full p-2.5 dark:bg-gray-600 dark:border-gray-500 
                    dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500"
                    placeholder={modalType === 'add' ? "Nhập tên danh mục" : categoryData.name}
                    required={true}
                    onChange={handleChange}
                  />
                </div>               
              </div>
              <button
                type="submit"
                className="text-white inline-flex items-center bg-blue-700 hover:bg-blue-800 focus:ring-4 
                focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 
                text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"
              >               
                {modalType === 'edit' ? 'Sửa': 'Tạo'}
              </button>
            </form>
          </div>
        </div>
      </div>
    </>
  );
};

export default CategoryFormModal;
