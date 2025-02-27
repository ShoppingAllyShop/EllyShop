//input:haha -> output: Haha
export const capitalizeFirstLetter = (str) => {
    return str.charAt(0).toUpperCase() + str.slice(1);
};

/*
  input: "HelloWorld", 5
  output: "Hello"
*/
export const getFirstCharsByLengthNumber = (str, lengthNumber) => {
    return str.slice(0, lengthNumber);
}
