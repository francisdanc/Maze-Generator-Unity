using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMaze : MonoBehaviour
{

    public int cellsize;
    public int mapSize;
    private int width;
    private int height;
    public Cell[] cells;
    public GameObject wall;
    public GameObject floor;
    // Start is called before the first frame update


    void Start()
    {
        height = mapSize;
        width = mapSize;
        cells = new Cell[width * height];
        int allVisited = 0;
        int index = 0;
        for(int z = 0; z < width; z++) { // 1
            for(int x = 0; x < height; x++) {
                
                Vector3 position = new Vector3(x * cellsize, 0, z * cellsize);
                Cell cell = new Cell(position, cellsize, index, wall, floor);
                cell.GenerateCell(position, cellsize);

                cells[index] = cell;
                index++;
            }
        }

        int CurrentCell = cells[Random.Range(0, cells.Length)].GetIndex();


        Generate();

        while (allVisited < width * height) {
            allVisited += Walk(cells, CurrentCell);
            CurrentCell = Hunt(cells, width, height);
            }
    }

    void Generate() {

            int allVisited = 0;
            int CurrentCell = cells[Random.Range(0, cells.Length)].GetIndex();

            //initialise a grid full of cells
       /* int index = 0;
        for(int z = 0; z < width; z++) { // 1
            for(int x = 0; x < height; x++) {
                
                Vector3 position = new Vector3(x * cellsize, 0, z * cellsize);
                Cell cell = new Cell(position, cellsize, index, wall, floor);
                cell.GenerateCell(position, cellsize);

                cells[index] = cell;
                index++;
            }
        }*/


            

    }



    int Walk(Cell[] cells, int CurrentCell) {
        bool AllVisited = false;
        int visited = 0;
        bool exitCondition = false;
        int loopcount = 0;

        string debug = CurrentCell.ToString();
        

        while(AllVisited == false) {
            int neighbourVisit = 0;

            List<int> neighbours = cells[CurrentCell].GetNeighbours(CurrentCell, width, height, cells);
            cells[CurrentCell].SetVisited(true);
            visited++;

            for(int x = neighbours.Count - 1; x >= 0; x--) {
                if(cells[neighbours[x]].IsVisited()){
                    neighbourVisit++;
                    neighbours.RemoveAt(x);
                }
            }
            
            if(neighbours.Count == 0) {
                break;
            }

            int nextCell = neighbours[Random.Range(0,neighbours.Count)];



            if(CurrentCell + 1 == nextCell) {
                cells[CurrentCell].DestroyWall(1);
                cells[nextCell].DestroyWall(3);
                //Debug.LogError("walk right from: " + debug  + " Where visited = " + cells[nextCell].IsVisited().ToString());
            } else if (CurrentCell + width == nextCell) {
                cells[CurrentCell].DestroyWall(0);
                cells[nextCell].DestroyWall(2);
                //Debug.LogError("walk up from: " + debug  + " Where visited = " + cells[nextCell].IsVisited().ToString());
            }else if(CurrentCell - 1 == nextCell) {
                cells[CurrentCell].DestroyWall(3);
                cells[nextCell].DestroyWall(1);
                //Debug.LogError("walk left from " + debug  + " Where visited = " + cells[nextCell].IsVisited().ToString());
            }else if(CurrentCell - width == nextCell) {
                cells[CurrentCell].DestroyWall(2);
                cells[nextCell].DestroyWall(0);
                //Debug.LogError("walk down from " + debug  + " Where visited = " + cells[nextCell].IsVisited().ToString());
            }


            if(neighbourVisit == 4) {
                AllVisited = true;
            }

            if(exitCondition == false && loopcount < width * height){
                loopcount++;
            } else if (loopcount >= width * height ) {
                //Debug.LogError("loop was too long");
                //throw new System.Exception("Maze Generator ran into an error");

            }

            CurrentCell = nextCell;

            debug = CurrentCell.ToString();
            

        }

    return visited;
        
    }

    int Hunt(Cell[] cells, int width, int height) {
        int index = 0;
        for (int z = 0; z < height; z++){
            for (int x = 0; x < width; x++) {
                List<int> neighbours = cells[index].GetNeighbours(cells[index].GetIndex(), width, height, cells);



                if(!cells[index].IsVisited()){

                    for (int n = neighbours.Count - 1; n >= 0; n--) {
                        if(!cells[neighbours[n]].IsVisited()){
                            neighbours.RemoveAt(n);
                        }
                    }

                    if(neighbours.Count == 0) {
                        continue;
                    }

                    int nextCell = neighbours[Random.Range(0,neighbours.Count)];
                    

                            if(cells[index].GetIndex() + 1 == nextCell) {
                                cells[index].DestroyWall(1);
                                cells[nextCell].DestroyWall(3);
                                //Debug.LogError("Hunt found: " + index.ToString() + "and connected to " + nextCell.ToString() + "Where visited = " + cells[nextCell].IsVisited().ToString());
                                return cells[index].GetIndex();
                            }else if(cells[index].GetIndex() + width == nextCell){
                                cells[index].DestroyWall(0);
                                cells[nextCell].DestroyWall(2); 
                                //Debug.LogError("Hunt found: " + index.ToString() + "and connected to " + nextCell.ToString() + "Where visited = " + cells[nextCell].IsVisited().ToString());
                                return cells[index].GetIndex();                              
                            }else if(cells[index].GetIndex() - 1 == nextCell){
                                cells[index].DestroyWall(3);
                                cells[nextCell].DestroyWall(1);
                                //Debug.LogError("Hunt found: " + index.ToString() + "and connected to " + nextCell.ToString() + "Where visited = " + cells[nextCell].IsVisited().ToString());
                                return cells[index].GetIndex();                                
                            }else if(cells[index].GetIndex() - width == nextCell) {
                                cells[index].DestroyWall(2);
                                cells[nextCell].DestroyWall(0);
                                //Debug.LogError("Hunt found: " + index.ToString() + "and connected to " + nextCell.ToString() + "Where visited = " + cells[nextCell].IsVisited().ToString());
                                return cells[index].GetIndex();
                            }
                         

                    }
                    
                    index++;
                }
            }
            return index;
        }
        
        
    }
    


