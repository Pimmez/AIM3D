public class ActivateDoor : CheckVisibility {

    protected override void Visible()
    {
        base.Visible();
        transform.parent.GetComponent<Doors>().DoorAngle();
    }
}
