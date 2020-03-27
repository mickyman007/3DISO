public interface IMovementRules {
    ISpace[] GetLegalMoves(IBoard board, IPiece piece);
}
