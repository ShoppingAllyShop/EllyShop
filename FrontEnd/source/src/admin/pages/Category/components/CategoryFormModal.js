import React, { useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import {
  setCreateCateButton,
  createNewCate,
  setAlert,
  triggerEditModal,
  updateCategory
} from "../../../../redux/slices/admin/categorySlice";
import useAxiosAuth from "../../../../api/useAxiosAuth";
import { BUTTON_NAME, BUTTON_TYPE, RESPONSE_API_STATUS } from "../../../../constants/common";
import { ADMIN_ENDPOINT } from "../../../../constants/endpoint";
import InputField from "../../../../components/Form/InputField";
import { useForm, Controller } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import { ALERT_TYPE } from "../../../../constants/enum";

const CategoryFormModal = ({ level, categoryData, modalType }) => {
  const dispatch = useDispatch();
  const auth = useAxiosAuth()
  const createEditModal = useSelector((state) => state.adminCategory.createEditModal);
  const selectedCategory = useSelector((state) => state.adminCategory.selectedCategory);
  const [formError, setformError] = useState("");
  const labelFormClass = "block mb-2 text-sm font-semibold text-gray-900 dark:text-white"
  const isEditForm = modalType === BUTTON_TYPE.EDIT

  const schema = yup.object({name: yup.string().required("Vui lòng điền tên category"), gender:yup.string()
  }).required();

  const {register, control , handleSubmit, formState: { errors }} = useForm({
    mode: "onSubmit",
    criteriaMode: "all",
    resolver: yupResolver(schema),
  });

  const handleShowResult = (content, type) => {
    dispatch(setAlert({isDisplay:true, content:content, type:type }));
    dispatch(triggerEditModal({isDisplay:false}))
    
    // Ẩn Alert sau 3 giây
    const timer = setTimeout(() => {
      dispatch(setAlert({isDisplay:false})); 
    }, 3000);

    // Dọn dẹp timeout nếu component cha unmount
    return () => clearTimeout(timer);
  };

  const handleCloseModal = async () => {
    dispatch(setCreateCateButton({ isDisplay: false }));
  };

  const onSubmitForm = async (data, e) => {
    e.preventDefault();

    if (isEditForm) {
      handleEditCategory(data);
      return;
    }
    handleAddCategory(data);
  };

  const handleAddCategory = async (data) => {
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
      name: data.name,
      categoryLevel: level,
      parentId: parentId
    };

    //Call login api
    const endpoint = ADMIN_ENDPOINT.CATEGORY_ADD;
    const [addResponse, ] = await Promise.all([
      auth.post(endpoint, newCate)
    ]);

    if (addResponse?.data?.status === RESPONSE_API_STATUS.SUCCESS) {
      handleShowResult("Bạn đã thêm danh mục mới thành công", ALERT_TYPE.SUCCESS)

       //add to store
      dispatch(createNewCate(newCate));
    }else if (addResponse?.data?.status === RESPONSE_API_STATUS.ERROR){
      setformError(addResponse?.data?.message)
    }
  };

  const handleEditCategory = async (data) => {
    let updatedCate = {
      id: selectedCategory[0].id,
      name: data.name,  
      categoryLevel: selectedCategory[0].categoryLevel,
      parentId: selectedCategory[0].parentId,
      gender: data.gender != null ? JSON.parse(data.gender) : data.gender
    };
  
    //Call login api
    const endpoint = ADMIN_ENDPOINT.CATEGORY_EDIT;
    const [editResponse] = await Promise.all([
      auth.post(endpoint, updatedCate)
    ]);

    if (editResponse?.data?.status === RESPONSE_API_STATUS.SUCCESS) {
      handleShowResult("Bạn đã thêm danh mục mới thành công", ALERT_TYPE.SUCCESS)

       //add to store
      dispatch(updateCategory(updatedCate));
    }else if (editResponse?.data?.status === RESPONSE_API_STATUS.ERROR){
      setformError(editResponse?.data?.message)
    }
  };

  const renderGenderSelectList = (field) => {
    return (
      <select {...field}
        className="mt-3 pl-1 font-semibold block py-2.5 px-0 w-full text-sm text-gray-900 bg-transparent border-0 border-b-2
       border-gray-200 appearance-none dark:text-gray-400 dark:border-gray-700 focus:outline-none focus:ring-0 focus:border-gray-200 peer"
      >
        <option defaultValue="" className="text-gray-900 ">
          Giới tính
        </option>
        <option value={true}>Nam</option>
        <option value={false}>Nữ</option>
      </select>
    );
  };

  return (
    <>
      <div
        tabIndex={-1}
        className="fixed inset-0 flex items-center justify-center z-50 bg-opacity-75 bg-gray-300 h-auto"
      >
        <div className="m-auto">
          <div className="relative bg-white rounded-lg shadow dark:bg-gray-700 w-[450px]">
            {/* Modal header */}
            <div className="flex items-center justify-between p-4 md:p-5 border-b rounded-t dark:border-gray-600">
              <h3 className="text-lg font-semibold text-gray-900 dark:text-white">
                {modalType === "add"
                  ? `Tạo danh mục cấp ${createEditModal.level}`
                  : `Chỉnh sửa danh mục ${createEditModal.level}`}
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
            {formError && (
            <p className="text-red-600 font-semibold text-normal text-center">
              {formError}
            </p>
          )}
            <form className="p-4 md:p-5" onSubmit={handleSubmit(onSubmitForm)}>
              <div className="grid gap-4 mb-4 grid-cols-2">
                <div className="col-span-2">
                  {/* <label htmlFor="name" className={labelFormClass}>Tên</label> */}
                  <InputField
                    label="Tên"
                    labelStyle={labelFormClass}
                    name="name"
                    register={register}
                    errors={errors}
                    defaultValue={isEditForm && selectedCategory[0].name != null ? selectedCategory[0].name : ''}
                    className="text-gray-900 text-sm rounded-lg block w-full p-2.5
                    bg-gray-50 border border-gray-300 focus:outline-none focus:ring-0 focus:border-gray-500"
                  />
                  <Controller 
                    name="gender"
                    control={control}
                    defaultValue={isEditForm && selectedCategory[0].gender != null ? selectedCategory[0].gender : ""}
                    render={({field}) => renderGenderSelectList(field)}
                  />                 
                </div>
              </div>
              <button
                type="submit"
                className="text-white inline-flex items-center bg-blue-700 hover:bg-blue-800 focus:ring-4 
                focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 
                text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"
              >
                {isEditForm ? BUTTON_NAME.EDIT : BUTTON_NAME.ADD}
              </button>
            </form>
          </div>
        </div>
      </div>      
    </>
  );
};

export default CategoryFormModal;
