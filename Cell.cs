using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public GameObject wall;
    public GameObject floor;


    public GameObject east_wallobj;
    public GameObject west_wallobj;
    public GameObject north_wallobj;
    public GameObject south_wallobj;


    private int index;
    private Vector3 position;
    private float cellsize;
    private bool visited = false;

    public Cell(Vector3 position, int cellsize, int index, GameObject wall, GameObject floor) {
        this.position = position;
        this.cellsize = cellsize;
        this.index = index;
        this.wall = wall;
        this.floor = floor;

    }

    public int GetIndex() {
        return index;
    }

    public bool IsVisited() {
        return visited;
    }

    public void SetVisited(bool IsVisited){
        visited = IsVisited;
    }

    public List<int> GetNeighbours(int cellIndex, int width, int height, Cell[] cells) {
        cellIndex = index;
        List<int> neighbours = new List<int>();
        if(cells[cellIndex].position.x == 0 && cells[cellIndex].position.z == 0) { // bottom left
            neighbours.Add(cells[cellIndex + 1].GetIndex());
            neighbours.Add(cells[cellIndex + width].GetIndex());
        } else if(cells[cellIndex].position.x > 0 && cells[cellIndex].position.z == 0 && cells[cellIndex].position.x < cellsize * (width - 1)) { // between bottom left and bottom right
            neighbours.Add(cells[cellIndex + 1].GetIndex());
            neighbours.Add(cells[cellIndex - 1].GetIndex());
            neighbours.Add(cells[cellIndex + width].GetIndex());
        } else if (cells[cellIndex].position.x == cellsize * (width - 1) && cells[cellIndex].position.z == 0) { //bottom right
            neighbours.Add(cells[cellIndex - 1].GetIndex());
            neighbours.Add(cells[cellIndex + width].GetIndex());
        } else if (cells[cellIndex].position.x == cellsize * (width - 1) && cells[cellIndex].position.z > 0 && cells[cellIndex].position.z < cellsize * (height - 1) ) {
            //between bottom right and top right
            neighbours.Add(cells[cellIndex - 1].GetIndex());
            neighbours.Add(cells[cellIndex - width].GetIndex());
            neighbours.Add(cells[cellIndex + width].GetIndex());
        } else if (cells[cellIndex].position.x == cellsize * (width - 1) && cells[cellIndex].position.z == cellsize * (height -1)) { // top right
            neighbours.Add(cells[cellIndex - 1].GetIndex());
            neighbours.Add(cells[cellIndex - width].GetIndex());
        } else if(cells[cellIndex].position.z == cellsize * (height - 1) && cells[cellIndex].position.x > 0 && cells[cellIndex].position.x < cellsize * (width - 1)) { //between top right and top left
            neighbours.Add(cells[cellIndex - 1].GetIndex());
            neighbours.Add(cells[cellIndex + 1].GetIndex());
            neighbours.Add(cells[cellIndex - width].GetIndex());
        } else if (cells[cellIndex].position.z == cellsize * (height - 1) && cells[cellIndex].position.x == 0) { // top left
            neighbours.Add(cells[cellIndex + 1].GetIndex());
            neighbours.Add(cells[cellIndex - width].GetIndex());
        }else if (cells[cellIndex].position.x == 0 && cells[cellIndex].position.z > 0 && cells[cellIndex].position.z < cellsize * (height - 1) ) { //between bottom left and top left
            neighbours.Add(cells[cellIndex + 1].GetIndex());
            neighbours.Add(cells[cellIndex + width].GetIndex());
            neighbours.Add(cells[cellIndex - width].GetIndex());
        } else { //every other cell
            neighbours.Add(cells[cellIndex + 1].GetIndex());
            neighbours.Add(cells[cellIndex + width].GetIndex());
            neighbours.Add(cells[cellIndex - width].GetIndex());
            neighbours.Add(cells[cellIndex - 1].GetIndex());
        }

        return neighbours;


    }

    public void GenerateCell(Vector3 position, int cellsize) {
        Vector3 floorposition = new Vector3(position.x, position.y - (cellsize / 2f), position.z);
        Vector3 ceilingposition = new Vector3(position.x, position.y + (cellsize / 2f), position.z);
        Vector3 east_wall_pos = new Vector3(position.x + (cellsize / 2f) , position.y, position.z);
        Vector3 west_wall_pos = new Vector3(position.x - (cellsize / 2f) , position.y, position.z);
        Vector3 north_wall_pos = new Vector3(position.x, position.y, position.z + (cellsize / 2f));
        Vector3 south_wall_pos = new Vector3(position.x, position.y, position.z - (cellsize / 2f));

        GameObject floorobj = Instantiate(floor, floorposition, Quaternion.identity);
        GameObject ceilingobj = Instantiate(floor, ceilingposition, Quaternion.identity);
        east_wallobj = Instantiate(wall, east_wall_pos, Quaternion.Euler(90,90,0));
        west_wallobj = Instantiate(wall, west_wall_pos, Quaternion.Euler(90,90,0));
        north_wallobj = Instantiate(wall, north_wall_pos, Quaternion.Euler(90, 0, 0));
        south_wallobj =Instantiate(wall, south_wall_pos, Quaternion.Euler(90, 0, 0));

        floorobj.transform.localScale = new Vector3(cellsize, 0.5f, cellsize);
        ceilingobj.transform.localScale = new Vector3(cellsize, 0.5f, cellsize);
        east_wallobj.transform.localScale = new Vector3(cellsize, 0.5f, cellsize);
        west_wallobj.transform.localScale = new Vector3(cellsize, 0.5f, cellsize);
        north_wallobj.transform.localScale = new Vector3(cellsize, 0.5f, cellsize);
        south_wallobj.transform.localScale = new Vector3(cellsize, 0.5f, cellsize);

    }

    public void DestroyWall(int wall) {
        if(wall == 0) {
            Destroy(north_wallobj);
        }else if(wall == 1) {
            Destroy(east_wallobj);
        }else if(wall ==2) {
            Destroy(south_wallobj);
        } else if(wall == 3){
            Destroy(west_wallobj);
        }
    }
    
}
