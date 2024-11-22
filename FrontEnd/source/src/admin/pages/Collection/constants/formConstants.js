export const COLLECTION_FORM_CONST = {
    VALIDATION_ERRORS: {
      REQUIRED: "Bắt buộc không để trống",
      EXITED_NAME:"Tên bộ sưu tập này đã có vui lòng chọn tên khác"
    },
    MESSAGES: {
      NOT_CHANGED: "Bạn chưa thay đổi thông tin",
      UPDATE_SUCCESS: (name) => `Bộ sưu tập ${name} cập nhật thành công`,
      CREATE_SUCCESS: "Tạo bộ sưu tập mới thành công",
      DELETE_CONFIRM: (name) => `Bạn có chắc muốn xóa bộ sưu tập ${name}?`,
      DELETE_SUCCESS: (name) => `Bộ sưu tập ${name} xóa thành công`
    },
    LABELS: {
      NAME: "Tên",
    },
    TITLES: {
      CREATE: "TẠO BỘ SƯU TẬP",
      UPDATE: "CẬP NHẬT BỘ SƯU TẬP"
    }
};
