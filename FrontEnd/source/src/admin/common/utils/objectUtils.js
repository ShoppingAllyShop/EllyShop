  /*
  input: "HelloWorld", 5
  output: "Hello"
*/
export const findObjectInObject = (largeObject, target) => {
    return Object.values(largeObject).find(
        obj => JSON.stringify(obj) === JSON.stringify(target)
      ) || null;
}