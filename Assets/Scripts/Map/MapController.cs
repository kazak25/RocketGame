using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private int _maxMapSize = 3;
    [SerializeField] private MapPiece _mapPiecePrefab;

    private MapPiece _currentMapPiece;
    private MonoBehaviourPool<MapPiece> _mapPiecesPool;


    private void Awake()
    {
        _mapPiecesPool = new MonoBehaviourPool<MapPiece>(_mapPiecePrefab, transform, _maxMapSize);
        _currentMapPiece = _mapPiecesPool.Take();
        _currentMapPiece.EndReached += CreateNewPiece;
    }


    private void CreateNewPiece()
    {
        if (_mapPiecesPool.UsedItems.Count >= _maxMapSize)
        {
            var firstItem = _mapPiecesPool.UsedItems.First();
            firstItem.EndReached -= CreateNewPiece;
            _mapPiecesPool.Release(firstItem);
        }

        var newPiece = _mapPiecesPool.Take();
        newPiece.transform.position =
            _currentMapPiece.transform.position + new Vector3(0, 48, 0);

        newPiece.EndReached += CreateNewPiece;
        _currentMapPiece = newPiece;
    }

    private void OnDestroy()
    {
        if (_currentMapPiece != null)
        {
            _currentMapPiece.EndReached -= CreateNewPiece;
        }

        foreach (var usedItem in _mapPiecesPool.UsedItems)
        {
            usedItem.EndReached -= CreateNewPiece;
        }
    }
}