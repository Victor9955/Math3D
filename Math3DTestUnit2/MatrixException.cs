using System.Numerics;

public class MatrixSumException<T> : Exception where T : INumber<T>
{
}

public class MatrixMultiplyException<T> : Exception where T : INumber<T>
{
}

public class MatrixScalarZeroException<T> : Exception where T : INumber<T>
{}

public class MatrixInvertException<T> : Exception where T : INumber<T>
{}