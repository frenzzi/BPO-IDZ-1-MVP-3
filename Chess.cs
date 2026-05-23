using System;

namespace IDZ_MVP.Chess
{
    public enum File : byte
    {
        None = 0,
        A = 1, B, C, D, E, F, G, H
    }

    public enum Rank : byte
    {
        None = 0,
        One = 1, Two, Three, Four, Five, Six, Seven, Eight
    }

    public readonly struct Coordinate
    {
        public File File { get; }
        public Rank Rank { get; }

        private Coordinate(File file, Rank rank)
        {
            File = file;
            Rank = rank;
        }

        public static bool TryCreate(string input, out Coordinate coord)
        {
            coord = default;
            if (string.IsNullOrEmpty(input) || input.Length != 2)
                return false;

            char f = char.ToUpper(input[0]);
            char r = input[1];

            if (f < 'A' || f > 'H' || r < '1' || r > '8')
                return false;

            coord = new Coordinate((File)(f - 'A' + 1), (Rank)(r - '0'));
            return true;
        }

        public override string ToString() => $"{File.ToString().ToLower()}{(int)Rank}";
    }

    public interface ISecondNameProvider
    {
        string SecondName();
    }

    public abstract class ChessPiece : ISecondNameProvider
    {
        private const string BlackStr = "Черные";
        private const string WhiteStr = "Белые";

        protected string _name;
        protected string _secondName;
        protected int _price;
        protected bool _isWhite;
        protected Coordinate _coordinate;

        public ChessPiece(bool isWhite, Coordinate coordinate)
        {
            _name = "";
            _secondName = "";
            _price = 1;
            _isWhite = isWhite;
            _coordinate = coordinate;
        }

        ~ChessPiece()
        {
            Console.WriteLine($"Фигура {this} срублена");
        }

        protected void AnnounceCreation()
        {
            Console.WriteLine($"Фигура {this} на доске");
        }

        public string SecondName() => _secondName;

        public override string ToString() =>
            $"{_name}, {(_isWhite ? WhiteStr : BlackStr)}, {_coordinate}, {_price}";
    }

    public abstract class LightPiece : ChessPiece
    {
        public LightPiece(bool isWhite, Coordinate coordinate) : base(isWhite, coordinate) { }
    }

    public abstract class HeavyPiece : ChessPiece
    {
        public HeavyPiece(bool isWhite, Coordinate coordinate) : base(isWhite, coordinate) { }
    }

    public class Pawn : ChessPiece
    {
        public Pawn(bool isWhite, Coordinate coordinate) : base(isWhite, coordinate)
        {
            _name = "Pawn";
            _secondName = "Пешка";
            _price = 1;
            AnnounceCreation();
        }
    }

    public class King : ChessPiece
    {
        public King(bool isWhite, Coordinate coordinate) : base(isWhite, coordinate)
        {
            _name = "King";
            _secondName = "Король";
            _price = 0;
            AnnounceCreation();
        }
    }

    public class Bishop : LightPiece
    {
        public Bishop(bool isWhite, Coordinate coordinate) : base(isWhite, coordinate)
        {
            _name = "Bishop";
            _secondName = "Слон";
            _price = 3;
            AnnounceCreation();
        }
    }

    public class Rook : HeavyPiece
    {
        public Rook(bool isWhite, Coordinate coordinate) : base(isWhite, coordinate)
        {
            _name = "Rook";
            _secondName = "Ладья";
            _price = 5;
            AnnounceCreation();
        }
    }

    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Шахматы");
            ChessPiece[] chessPieces = new ChessPiece[4];

            Coordinate.TryCreate("a1", out Coordinate kingCoordinate);
            King king = new King(false, kingCoordinate);
            chessPieces[0] = king;

            Coordinate.TryCreate("a2", out Coordinate pawnCoordinate);
            Pawn pawn = new Pawn(true, pawnCoordinate);
            chessPieces[1] = pawn;

            Coordinate.TryCreate("a3", out Coordinate rookCoordinate);
            Rook rook = new Rook(false, rookCoordinate);
            chessPieces[2] = rook;

            Coordinate.TryCreate("a4", out Coordinate bishopCoordinate);
            Bishop bishop = new Bishop(true, bishopCoordinate);
            chessPieces[3] = bishop;

            Console.WriteLine();

            foreach(ChessPiece piece in chessPieces)
            {
                Console.WriteLine(piece);
                Console.WriteLine(piece.SecondName());
            }
        }
    }
}