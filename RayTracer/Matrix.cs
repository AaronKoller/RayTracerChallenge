using System;
using System.Data;
using RayTracer.Transformations;

//https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.icollection-1?view=netframework-4.8

namespace RayTracer
{
    public class Matrix
    {
        private  double[,] _data;
        private readonly bool _isIdentity;

        public Matrix()
        {

        }

        public Matrix(Tuple tuple)
        {
            _data = new Matrix(new [,]
            {
                {tuple.X },
                {tuple.Y },
                {tuple.Z },
                {tuple.W },
            }).Data;
        }
            

        public Matrix(double[,] data)
        {
            _data = data;
        }

        public int Rows => _data.GetLength(0);
        public int Columns => _data.GetLength(1);

        public double[,] Data => _data;
        public bool IsIdentity { get; set; }

        public Matrix Identity => GenerateIdentityMatrix();

        public static Matrix Transform => Tuple.Zero.IdentityMatrix;


        public Matrix GenerateIdentityMatrix(int size = 4)
        {

            if(size == 0)
            {
                size = Columns > Rows ? Columns : Rows;
            }

            Matrix identityMatrix = new Matrix(new double[size, size]);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == j)
                    {
                        identityMatrix.Data[i, j] = 1;
                    }
                }
            }

            identityMatrix.IsIdentity = true;
            return identityMatrix;
        }

        public static bool operator ==(Matrix matrix1, Matrix matrix2)
        {
            return CompareMatrices(matrix1, matrix2);
        }


        public static bool operator !=(Matrix matrix1, Matrix matrix2)
        {
            return !CompareMatrices(matrix1, matrix2);
        }


        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {

            //https://stackoverflow.com/questions/20181020/how-to-multiply-a-matrix-in-c

            int rows1 = matrix1.Rows;
            int columns1 = matrix1.Columns;

            int rows2 = matrix2.Rows;
            int columns2 = matrix2.Columns;


            double[,] resultData = new double[rows1, columns2];

            for (int i = 0; i < rows1; i++)
            {
                for (int j = 0; j < columns2; j++)
                {
                    for (int k = 0; k < columns1; k++)
                    {
                        resultData[i, j] += matrix1.Data[i, k] * matrix2.Data[k, j];
                    }
                }
            }

            int maxColumns = 0;

            if (matrix1.IsIdentity)
            {
                maxColumns = matrix2.Data.GetLength(1);
            }
            else if (matrix2.IsIdentity)
            {
                maxColumns = matrix1.Data.GetLength(1);
            }

            if (maxColumns > 0)
            {
                resultData = KeepColumns(resultData, maxColumns);
            }

            return new Matrix(resultData);
        }

        private static double[,] KeepColumns(double[,] inputArray, int numOfColumnsToKeep)
        {
            int rows = inputArray.GetLength(0);

            double[,] resizedArray = new double[rows, numOfColumnsToKeep];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < numOfColumnsToKeep; j++)
                {
                    resizedArray[i, j] = inputArray[i, j];
                }

            }

            return resizedArray;
        }

        private static bool CompareMatrices(Matrix matrix1, Matrix matrix2)
        {
            if (!AreMatricesOfEqualSize(matrix1, matrix2))
            {
                return false;
            }

            double[,] data1 = matrix1.Data;
            double[,] data2 = matrix2.Data;


            for (int i = 0; i < data1.GetLength(0); i++)
            {
                for (int j = 0; j < data2.GetLength(1); j++)
                {
                    if (!data1[i, j].Equals(data2[i, j]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        private static bool AreMatricesOfEqualSize(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.Data.GetLength(0) != matrix2.Data.GetLength(0))
            {
                throw new MatrixSizeMismatch("The matrices do not have the same number of rows.");
            }

            if (matrix1.Data.GetLength(1) != matrix2.Data.GetLength(1))
            {
                throw new MatrixSizeMismatch("The matrices do not have the same number of columns.");
            }

            return true;
        }

        public Matrix Transpose()
        {
            var transposedData = new double[Columns, Rows];

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    transposedData[j, i] = _data[i, j];
                }
            }
            this._data = transposedData;
            return this;
        }

        public double Determinant()
        {

            //determinant must have column=row AND must be greater than 1
            var data = _data;
            var determinant = 0.0;
            if (Rows < 2 || Columns < 2) throw new MatrixSizeTooSmall("A determinant must be calculated on a matrix with 2 or more columns and rows.");
            if (Rows != Columns)
                throw new NonSquareMatrix("A determinant must be calculated on a matrix with an equal number of rows and columns.");

            if (Columns == 2)
            {
                determinant = data[0, 0] * data[1, 1] - data[1, 0] * data[0, 1];
            }
            else
            {
                for (int i = 0; i < Columns; i++)
                {
                    determinant += data[0, i] * this.CoFactor(0, i);
                }
            }
            return determinant;
        }

        public double CoFactor(int excludedRow, int excludedColumn)
        {
            var minor = this.Minor(excludedRow, excludedColumn);
            return (excludedColumn + excludedRow) % 2 == 0 ? minor : minor * -1;
        }

        public double Minor(int excludedRow, int excludedColumn)
        {
            return this.SubMatrix(excludedRow, excludedColumn).Determinant();
        }

        public Matrix SubMatrix(int excludedRow, int excludedColumn)
        {
            if (Rows < 2 || Columns < 2) throw new MatrixSizeTooSmall("The submatrix can not be performed on a matrix that has one column or one row");
            if (excludedRow > Rows || excludedColumn > Columns) throw new ExclusionColumnOrRowTooBig("The excluded column or row was of an index larger then the matrix being converted into a submatrix.");
            var subMatrix = new double[Rows - 1, Columns -1];

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    var subMatrixRow = 0;
                    var subMatrixColumn = 0;
                    if(i == excludedRow || j == excludedColumn) continue;
                    if (i < excludedRow) subMatrixRow = i;
                    if (i > excludedRow) subMatrixRow = i - 1;
                    if (j < excludedColumn) subMatrixColumn = j;
                    if (j > excludedColumn) subMatrixColumn = j - 1;
                    subMatrix[subMatrixRow, subMatrixColumn] = _data[i, j];
                }
            }
            return new Matrix(subMatrix);
        }

        public Matrix Inverse()
        {
            var determinant = this.Determinant();
            //if(determinant == 0) then throw
            var inverseMatrix = new Matrix(new double[Rows,Columns]);

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    var cofactor = CoFactor(i, j);
                    var inverse = cofactor / determinant;
                    inverseMatrix.Data[j, i] = inverse;
                }
            }

            return inverseMatrix;

        }

        public Matrix ViewTransform(Tuple from, Tuple to, Tuple up)
        {
            var forward = (to - from).Normalize();
            var left = forward.Cross(up.Normalize());
            var trueUp = left.Cross(forward);
            var orientation = new Matrix(new double[,]
            {
                {left.X    ,  left.Y   , left.Z    , 0},
                {trueUp.X  ,  trueUp.Y , trueUp.Z  , 0},
                {-forward.X, -forward.Y, -forward.Z, 0},
                {0         , 0         , 0         , 1},
            });

            var result = orientation * Transform.Translation(-from.X, -from.Y, -from.Z);
            return result;
        }
    }

    public class MatrixSizeMismatch : Exception
    {
        public MatrixSizeMismatch(string message) : base(message) { }
    }

    public class NonSquareMatrix : Exception
    {
        public NonSquareMatrix(string message) : base(message) { }
    }

    public class MatrixSizeTooSmall : Exception
    {
        public MatrixSizeTooSmall(string message) : base(message) { }
    }

    public class ExclusionColumnOrRowTooBig : Exception
    {
        public ExclusionColumnOrRowTooBig(string message) : base(message) { }
    }
}
