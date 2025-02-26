/*
  input: "1000000", 
  output: "1,000,000"
*/
export const formatCurrency = (number) => {
    return number !== null ? new Intl.NumberFormat().format(number) : "";
  }
//   export const formatCurrency = (number) => {
//     return new Intl.NumberFormat('vi-VN', {
//         style: 'currency',
//         currency: 'VND',
//       }).format(number);
//   }