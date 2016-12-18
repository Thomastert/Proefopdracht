using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChunkManager : MonoBehaviour
{
    [Header("Chunk prefabs")]
    public List<GameObject> m_chunks = new List<GameObject>();
    private bool added = false;
    //screen width in game unit
    private float m_screenWidthGameUnits;
    private List<GameObject> m_chunkClones = new List<GameObject>();
    public static float screenMoveSpeed = 6;

    void Start()
    {

        m_screenWidthGameUnits = getHalfScreenWidth();


        for (int i = 0; i < 3; i++)
        {
            m_chunkClones.Add(getRandomChunk(Vector3.zero));
        }
        for (int j = 0; j < m_chunkClones.Count; j++)
        {
            m_chunkClones[j].transform.position = new Vector3(j * getChunkWidth(m_chunkClones[j]), 0);
        }

        m_chunkClones[0].transform.position = new Vector3(0 - m_screenWidthGameUnits, 0);
        Vector3 eersteChunkPos = m_chunkClones[0].transform.position;

        for (int k = 0; k < m_chunkClones.Count; k++)
        {
            m_chunkClones[k].transform.position = new Vector3(eersteChunkPos.x + getChunkWidth(m_chunkClones[k]) * k, 0);
        }


    }

    void Update()
    {
        if (added)
        {
                added = false;
                m_chunkClones.Add(getRandomChunk(Vector3.zero));
        }
        sortChunks(m_chunkClones);
        for (int i = 0; i < m_chunkClones.Count; i++)
        {
            moveChunk(m_chunkClones[i], screenMoveSpeed);

            if (checkBoundsChunk(m_chunkClones[i]))
            {
                Destroy(m_chunkClones[i]);
                m_chunkClones.RemoveAt(i);
                added = true;
            }
        }
    }

    private void sortChunks(List<GameObject> _chunks)
    {
        if (_chunks.Count < 1)
        {
            Debug.Log("Error sort chunk! list heeft geen elementen");
            return;
        }
        var l_firstChunkV3 = _chunks[0].transform.position;
        for (int i = 0; i < _chunks.Count; i++)
        {
            _chunks[i].transform.position = new Vector3(l_firstChunkV3.x + (getChunkWidth(_chunks[i]) * i), 0);
        }
    }

    private bool checkBoundsChunk(GameObject _chunk)
    {
        if (_chunk.transform.position.x < 0 - (m_screenWidthGameUnits + getChunkWidth(_chunk) / 2))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void moveChunk(GameObject _chunk, float _speed)
    {
        _chunk.transform.position -= new Vector3(_speed * Time.deltaTime, 0);
    }

    private GameObject getRandomChunk(Vector3 _position)
    {
            return spawnChunk(m_chunks[Random.Range(0, m_chunks.Count)], _position);
    }

    private GameObject spawnChunk(GameObject _chunk, Vector3 _position)
    {
        return (GameObject)Instantiate(_chunk, _position, Quaternion.identity);
    }

    private float getChunkWidth(GameObject _chunk)
    {
        return _chunk.GetComponent<BoxCollider2D>().size.x;
    }

    private float getHalfScreenWidth()
    {
        return (Camera.main.orthographicSize / Camera.main.pixelHeight * Camera.main.pixelWidth);
    }
}