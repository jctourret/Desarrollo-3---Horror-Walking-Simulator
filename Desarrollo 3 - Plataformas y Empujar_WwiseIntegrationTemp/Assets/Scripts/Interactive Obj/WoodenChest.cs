using UnityEngine;

class WoodenChest : InteractiveObject
{
    public override void Update()
    {
        if (stateObject == StateObject.Available && inRange == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                stateObject = StateObject.Open;
                UIposter.SetActive(false);
                animator.SetTrigger("Open");
            }
        }
    }

    public void DropItem()
    {
        LoaderManager.Get().SpawnSpecialItem(this.transform.position, CameraBehaviour.GetCamera());
    }
}
