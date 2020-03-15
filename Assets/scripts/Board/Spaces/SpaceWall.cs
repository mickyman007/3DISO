public class SpaceWall : Space {
    public override void Initialise(int x, int y) {
        base.Initialise(x, y);
        CanSelect = false;
        CanMoveTo = false;
    }
}
