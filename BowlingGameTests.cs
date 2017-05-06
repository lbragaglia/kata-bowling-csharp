using Xunit;

namespace kata_bowling_csharp
{
    public class BowlingGameTest
    {
        private readonly BowlingGame _game;

        public BowlingGameTest()
        {
            _game = new BowlingGame();
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)]
        [InlineData(6, 0, 1, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)]
        [InlineData(10, 1, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)]
        [InlineData(12, 1, 9, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)]
        [InlineData(13, 1, 9, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)]
        [InlineData(10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)]
        [InlineData(12, 10, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)]
        [InlineData(14, 10, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)]
        [InlineData(15, 10, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)]
        [InlineData(11, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 5, 1)]
        [InlineData(16, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 5, 1)]
        [InlineData(300, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10)]
        [InlineData(90, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0)]
        [InlineData(150, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5)]
        public void TestScore(int expectedScore, params int[] rolls)
        {
            _game.Rolls(rolls);
            Assert.Equal(expectedScore, _game.Score());
        }
    }

    internal class BowlingGame
    {
        private int[] _rolls;

        public int Score()
        {
            var score = 0;
            for (int i = 0, currentFrame = 1; currentFrame <= 10; currentFrame += 1)
                score += GetScoreOfFrameStartingAtAndMoveToNext(ref i);
            return score;
        }

        private int GetScoreOfFrameStartingAtAndMoveToNext(ref int i)
        {
            if (IsStrike(i))
            {
                i += 1;
                return _rolls[i - 1] + _rolls[i] + _rolls[i + 1];
            }
            if (IsSpare(i))
            {
                i += 2;
                return _rolls[i - 2] + _rolls[i - 1] + _rolls[i];
            }
            i += 2;
            return _rolls[i - 2] + _rolls[i - 1];
        }

        private bool IsStrike(int i) => _rolls[i] == 10;

        private bool IsSpare(int i) => (_rolls[i] + _rolls[i + 1]) == 10;

        public void Rolls(params int[] rolls)
        {
            _rolls = rolls;
        }
    }
}
