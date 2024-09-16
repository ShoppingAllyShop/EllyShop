/**
 * Sắp xếp mảng các đối tượng theo thuộc tính cụ thể.
 * @param {Array} array - Mảng các đối tượng cần sắp xếp.
 * @param {string} property - Tên thuộc tính cần sắp xếp.
 * @param {boolean} ascending - `true` để sắp xếp tăng dần, `false` để sắp xếp giảm dần.
 * @returns {Array} - Mảng đã được sắp xếp.
 */
export const sortArrayByProp = (array, property, ascending = true) => {
  return array.sort((a, b) => {
    if (a[property] < b[property]) {
      return ascending ? -1 : 1;
    }
    if (a[property] > b[property]) {
      return ascending ? 1 : -1;
    }
    return 0;
  });
};
