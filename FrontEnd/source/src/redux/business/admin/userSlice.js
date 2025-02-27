export const updateItem = (state, updatedItem) => {
  const itemIndex = state.findIndex((item) => item.userId === updatedItem.id); // Tìm index của item
  if (itemIndex !== -1) {
    // Cập nhật item dựa trên index tìm được
    state[itemIndex] = {
      ...state[itemIndex], // giữ lại các thuộc tính cũ
      ...updatedItem, // cập nhật các thuộc tính mới
    };
  }
};

export const addItem = (state, data) => {
    state.data.userData.userList = [data.newItem, ...state.data.userData.userList ]
    if(data.isRemoveLastItem) state.data.userData.userList.pop()
  };
