using Cecs475.BoardGames.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cecs475.BoardGames.ComputerOpponent {
	internal struct MinimaxBestMove {
		public long Weight { get; set; }
		public IGameMove Move { get; set; }
	}

	public class MinimaxAi : IGameAi {
		private int mMaxDepth;
		public MinimaxAi(int maxDepth) {
			mMaxDepth = maxDepth;
		}

		public IGameMove FindBestMove(IGameBoard b) {
			return FindBestMove(b,
				true ? long.MinValue : long.MaxValue,
				true ? long.MaxValue : long.MinValue,
				mMaxDepth).Move;
		}

		private static MinimaxBestMove FindBestMove(IGameBoard b, long alpha, long beta, int depthLeft) {


			if (depthLeft == 0 || b.IsFinished == true)
			{
				return new MinimaxBestMove()
				{
					Weight = b.BoardWeight,
					Move = null
				};
			}

			else
			{
				MinimaxBestMove move = new MinimaxBestMove();
				move.Move = null;
				move.Weight = -1;
				long w = 0;
				foreach (var m in b.GetPossibleMoves())
				{
					b.ApplyMove(m);
					w = FindBestMove(b, 0, 0, depthLeft - 1).Weight;
					b.UndoLastMove();

					if ((b.CurrentPlayer == 2) && w > move.Weight)
					{
						move.Weight = w;
						move.Move = m;
					}

					else if ((b.CurrentPlayer == 1) && w < move.Weight)
					{
						move.Weight = w;
						move.Move = m;
					}
				}

				return move;
			}

		}

	}
}
