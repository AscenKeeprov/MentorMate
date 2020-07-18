
# Problem Description

"Green vs. Red" is a game played on a two-dimensional grid of cells. Each cell can be either green (represented by the number 1) or red (0). The game always receives parameters for the initial state of the grid which we'll call "Generation Zero". Subsequent states are generated based on the following rules:

1. Each red cell surrounded by exactly 3 or exactly 6 green cells must turn green  
2. A red cell must stay red if it has either 0, 1, 2, 4, 5, 7 or 8 green neighbours  
3. Each green cell surrounded by 0, 1, 4, 5, 7 or 8 green neighbours must become red  
4. A green cell must remain green if it has either 2, 3 or 6 green neighbours  
________
__Task__:

Create a program that accepts:

➔ The dimensions of the grid in cells - **`W`** (width) and **`H`** (height)  
➔ Coordinates **`X`** and **`Y`** specifying a cell to be observed  
➔ A number **`N`** denoting the total amount of states that should be generated  

As soon as the dimensions of the grid are specified, "Generation Zero" comprising sequences of zeroes and ones should be displayed on screen. Afterwards, a cell will be picked based on the provided coordinates. The aim of the program is to calculate how many times the target cell is green from generation 0 to generation N, both included, then print the result in the console.

Make sure that the program uses several classes, a uniform naming convention, some useful comments and documentation. This will demonstrate your knowledge of OOP and good coding practices thus accruing more points during evaluation.
___________________
__Important notes__:

- In theory, the grid can be infinite. For the sake of computational performance as well as human life expectancy, let's assume that **`W ≤ H < 1,000`**  
- Each cell may be surrounded by up to 8 other cells - 4 on the sides and 4 on the corners. This number varies for cells positioned along the edges of the grid  
- All rules should be applied to the whole grid at the same time in order for the next state to be generated  
____________
__Examples__:

**I**. 3x3 grid, coordinates (1,0), 10 generations. Expected output:  
`000`  
`111`  
`000`  
`5`  

**II**. 4x4 grid, coordinates (2,2), 15 generations. Expected output:  
`1001`  
`1111`  
`0100`  
`1010`  
`14`  
