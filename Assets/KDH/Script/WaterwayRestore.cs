using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class WaterwayRestore : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject dragEffectPrefab;
    public Image completePlate;

    private bool isDrawing = false;
    private Node startNode;
    private LineRenderer currentLine;
    private GameObject currentDragEffect;

    void Update()
    {
        if (isDrawing)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            currentLine.SetPosition(1, mousePosition);

            if (currentDragEffect != null)
            {
                currentDragEffect.transform.position = mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                StopDrawingLine(null);
            }
        }
    }

    public void StartDrawingLine(Node node)
    {
        if (isDrawing) return;

        isDrawing = true;
        startNode = node;

        // 라인 프리팹 생성
        GameObject lineObj = Instantiate(linePrefab);
        currentLine = lineObj.GetComponent<LineRenderer>();

        // 라인의 시작점을 시작 노드의 위치로 설정
        Vector3 startPosition = startNode.transform.position;
        startPosition.z = 0;
        currentLine.SetPosition(0, startPosition);
        currentLine.SetPosition(1, startPosition); // 끝점도 일단 시작점에

        // 드래그 이펙트 생성
        if (dragEffectPrefab != null)
        {
            currentDragEffect = Instantiate(dragEffectPrefab, startPosition, Quaternion.identity);
        }
    }

    // Node가 이 함수를 호출하거나, Update에서 취소될 때 호출됨
    public void StopDrawingLine(Node endNode)
    {
        if (!isDrawing) return;

        // 드래그 이펙트 삭제
        if (currentDragEffect != null)
        {
            Destroy(currentDragEffect);
        }

        // 연결 성공: 시작 노드가 아닌 다른 유효한 노드 위에서 마우스를 뗐을 때
        if (endNode != null && endNode != startNode)
        {
            // 라인의 끝점을 도착 노드의 위치에 정확히 맞춤
            Vector3 endPosition = endNode.transform.position;
            endPosition.z = 0;
            currentLine.SetPosition(1, endPosition);

            // TODO: 여기에 연결 성공 시 필요한 로직 추가 (예: 두 노드 정보 저장)
            Debug.Log(startNode.name + "와(과) " + endNode.name + " 연결 성공!");
        }
        // 연결 실패: 허공에 마우스를 떼거나, 시작 노드 위에서 뗐을 때
        else
        {
            // 그리던 라인을 파괴
            Destroy(currentLine.gameObject);
            Debug.Log("연결 실패, 라인을 삭제합니다.");
        }

        // 상태 초기화
        isDrawing = false;
        startNode = null;
        currentLine = null;
    }
    void Complete()
    {
        Debug.Log("보상 지급");
        completePlate.gameObject.SetActive(true);
    }

    public void Return()
    {
        Destroy(gameObject);
    }
}
