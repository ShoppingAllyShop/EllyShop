export const updatedCollection = (state, updatedCollection) => {
  const itemIndex = state.findIndex((item) => item.id === updatedCollection.id); // Tìm index của item
  if (itemIndex !== -1) {
    // Cập nhật item dựa trên index tìm được
    state[itemIndex] = {
      ...state[itemIndex], // giữ lại các thuộc tính cũ
      ...updatedCollection, // cập nhật các thuộc tính mới
    };
  }
};

export const addItem = (state, data) => {
    state.data.collectionData.collectionList = [data.newItem, ...state.data.collectionData.collectionList]
    if(data.isRemoveLastItem) state.data.collectionData.collectionList.pop()
  };