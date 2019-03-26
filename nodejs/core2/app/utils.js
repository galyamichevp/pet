// utils.js
function multiply(num1, num2) {
    console.log('Multiply:', num1, num2);
    return num1 * num2;
}
function divide(num1, num2) {
    console.log('Divide:', num1, num2);
    return num1 / num2;
}
// This is a private function
function privateLog() {
    console.log('Private Function');
}
export { multiply, divide };