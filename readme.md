# 

The spirit of this assignment is to do everything as well as you can from scratch. 
I.e. if there is functionality-built in, that will solve the problem quickly, then that is not what we want you to do. Use your skill and knowledge and the concepts we have been covering to solve these tasks (Under no circumstances use Linq or similar solutions).

Your judgment is an important part of your submission, so it is up to you to decide when your submission is good enough. 


>**Submit URL to your git repository.**   
Note that moving forward, we are stricter about the little things.   
Make it work, make it right, make it better. In that order.


## Task : Functions are a popping
Create the following functions: 
- A function that returns the square of a number
- A function that returns a length in mm assuming it has been given a length in inches.
- A function that returns the root of a number
- A function that returns the cube of a number 
- A function that returns the area of a circle given the radius. 
- A function that returns a greeting, given a name.

## Task : Flatten those numbers

In the file `arrays.json` there is an example of a an array that contains integers and possibly other arrays. If you need more test data you can get as many as you want from [this source](https://crismo-turquoisejaguar.web.val.run/arrayI) The task is to write a function that returns a flattened version of any such array structure.

**NB!** You might be tempted to use string manipulation, that is considered not solving the task. This is a jagged array of integers, not a string.


Example:
```code
[
  435,
  2028,
  [
    [
      3047, 4910, 8146,
      7999, 1433, 7211,
      1197, 6002
    ],
    2821,
    3508
  ]
]
//--> 
[435, 2028, 3047, 4910, 8146, 7999, 1433, 7211, 1197, 6002, 2821, 3508]
```

## Task : Left and right up and down, away we go. 

In the file `nodes.json`there is an example of a data structure that has a value of type integer, but also a left and right variable that could be a object of the same type. If you need more test data you can get as much as you want from [this source](https://crismo-turquoisejaguar.web.val.run/treeI)

- Calculate the sum of the full structure.
- Report the deepest level of the structure. 
- Report the number of nodes.


```code
{
  value: 67, // Level 1
  left: { value: 765, left: null, right: null }, // Level 2
  right: {
    value: 167, // Level 2
    left: {
      value: 564, // Level 3
      left: null,
      right: { value: 379, left: null, right: null } // Level 4
    },
    right: null
  }
}
// --> 
Sum = 1942
Deepest level = 4
Nodes = 5
```

## Task: My books they are a mess. 

In the file `books.json` there is a listing of books. Write the functions to
- Return only books starting with `The`
- Return only books written by authors with a `t` in their name
- The number of books written after `1992`
- The number of books written before `2004`
- Return the isbn number of all the books for a given author.
- List books alphabetically assending or decendig 
- List books chronologically assending or decendig 
- List books grouped by author last name
- Lits books grouped by author first name

