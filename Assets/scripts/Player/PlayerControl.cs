using UnityEngine;

public class PlayerControl : MonoBehaviour {

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Click();
        }
    }

    private void Click() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100.0f)) {
            if (hit.transform.TryGetComponent<ISpace>(out var space)) {
                if (space.CanSelect) {
                    space.IsSelected = !space.IsSelected;
                    return;
                }
            }

            if (hit.transform.TryGetComponent<IPiece>(out var piece)) {
                piece.IsSelected = !piece.IsSelected;
                return;
            }
        }
    }
}
