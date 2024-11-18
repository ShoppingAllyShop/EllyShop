import React, { useCallback, useRef, useState } from "react";
import { BUTTON_TYPE, FORM_ELEMENTS } from "../../../../constants/common";
import { debounce } from "../../../../utils/timeUtil";
import { useDispatch } from "react-redux";
import { setCreateUpdateUserModal } from "../../../../redux/slices/admin/userSlice";

const TopPaging = ({ data, paging, onSearchEmployeeUser }) => {
  const dispatch = useDispatch()
  const [searchValue, setSearchValue] = useState("");
  const selectedPageSizeRef = useRef(null)

  const debouncedSearch = useCallback(
    debounce((e) => {
      handleSearchEmployeeUser(e, FORM_ELEMENTS.SEARCH_INPUT);
    }, 200),
    []
  );

  const handleSearchEmployeeUser = (e, type) => {
    
    let params = {
      pageNumber: 1,
      pageSize: paging?.pageSize,
      sortBy: paging?.sortBy,
      sortOrder: paging?.sortOrder,
      searchInput: searchValue,
    };

    switch (type) {
      case BUTTON_TYPE.PAGE_SIZE_SELECTION:
        params.pageSize = e.target.value;
        break;
      case FORM_ELEMENTS.SEARCH_INPUT:     
        params.searchInput = e.target.value;
        params.pageSize = selectedPageSizeRef.current.value        
        break;
      default:
        break;
    }

    //call back parent
    onSearchEmployeeUser(params);
  };

  const hanldeClickAddBtn = () => {
    const {positions, departments , roles} = data

    let modalData = {
      positions: positions,
      departments: departments,
      roles: roles
    }
    dispatch(setCreateUpdateUserModal({isShow: true, type: "Add", data: modalData}))
  };

  const renderSelectQuantityNumber = () => {
    return (
      <form className="flex page-size-selection">
        <label htmlFor="sizes" className="block text-sm font-semibold text-gray-900 dark:text-white text-nowrap place-content-center mr-2">Số lượng</label>
        <select
          ref={selectedPageSizeRef}
          defaultValue={10}
          onChange={(e) => handleSearchEmployeeUser(e, BUTTON_TYPE.PAGE_SIZE_SELECTION)}
          id="sizes"
          className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-1.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
        >
          <option value={5}>5</option>
          <option value={10}>10</option>
          <option value={15}>15</option>
        </select>
      </form>
    );
  };

  const handleSearchChange = (event) => {
    const value = event.target.value;
    setSearchValue(value);
    
    // Gọi hàm debouncedSearch với giá trị nhập liệu mới
    debouncedSearch(event);
  };

  const renderSearch = () => {
    return (
      <form className="flex items-center">
        <label htmlFor="simple-search" className="sr-only">
          Tìm...
        </label>
        <div className="relative w-full">
          <div className="absolute inset-y-0 left-0 flex items-center pl-3 pointer-events-none">
            <i className="fa-solid fa-magnifying-glass"></i>
          </div>
          <input
            type="search"
            id="simple-search"
            className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-500 focus:border-primary-500 block w-full pl-10 p-2 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500"
            placeholder="Tìm ...."
            required=""
            value={searchValue}
            onChange={handleSearchChange}
          />
        </div>
      </form>
    );
  };

  return (
    <div className="flex flex-col md:flex-row items-center justify-between space-y-3 md:space-y-0 md:space-x-4 p-4">
      <div className="w-full md:w-1/2">{renderSearch()}</div>
      <div className="w-full md:w-auto flex flex-col md:flex-row space-y-2 md:space-y-0 items-stretch md:items-center justify-end md:space-x-3 flex-shrink-0">
        <button type="button" 
          className="flex items-center justify-center text-white bg-blue-700 hover:bg-blue-800
           focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-4 py-2 dark:bg-blue-600
            dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800"
          onClick={hanldeClickAddBtn }
        >
          <i className="fa-solid fa-user-plus mr-2"></i>
          Thêm
        </button>
        
        {renderSelectQuantityNumber()}
      </div>
    </div>
  );
};

export default TopPaging;
