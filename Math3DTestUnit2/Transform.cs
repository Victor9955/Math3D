    using System.Numerics;

    namespace Maths_Matrices.Tests;

    public class Transform
    {
        public Vector3<float> LocalPosition;
        public Vector3<float> WorldPosition
        {
            get
            {
                return new Vector3<float>(LocalToWorldMatrix[0, 3], LocalToWorldMatrix[1, 3], LocalToWorldMatrix[2, 3]);
            }
            set
            {
                LocalPosition = parent != null ? parent.WorldToLocalMatrix * new Vector4<float>(value.x, value.y, value.z, 1) : value;
            }
        }


        public Matrix<float> LocalTranslationMatrix
        {
            get
            {
                Matrix<float> result = Matrix<float>.Identity(4);
                result[0, 3] = LocalPosition.x;
                result[1, 3] = LocalPosition.y;
                result[2, 3] = LocalPosition.z;
                return result;
            }
        }

        public Vector3<float> LocalRotation;

        public Matrix<float> LocalRotationMatrix
        {
            get { return LocalRotationYMatrix * LocalRotationXMatrix * LocalRotationZMatrix; }
        }

        public Matrix<float> LocalRotationXMatrix
        {
            get
            {
                Matrix<float> rotationX = Matrix<float>.Identity(4);
                rotationX[1, 1] = float.Cos(float.DegreesToRadians(LocalRotation.x));
                rotationX[1, 2] = -float.Sin(float.DegreesToRadians(LocalRotation.x));
                rotationX[2, 1] = float.Sin(float.DegreesToRadians(LocalRotation.x));
                rotationX[2, 2] = float.Cos(float.DegreesToRadians(LocalRotation.x));
                return rotationX;
            }
        }

        public Matrix<float> LocalRotationYMatrix
        {
            get
            {
                Matrix<float> rotationY = Matrix<float>.Identity(4);
                rotationY[0, 0] = float.Cos(float.DegreesToRadians(LocalRotation.y));
                rotationY[2, 0] = -float.Sin(float.DegreesToRadians(LocalRotation.y));
                rotationY[0, 2] = float.Sin(float.DegreesToRadians(LocalRotation.y));
                rotationY[2, 2] = float.Cos(float.DegreesToRadians(LocalRotation.y));
                return rotationY;
            }
        }

        public Matrix<float> LocalRotationZMatrix
        {
            get
            {
                Matrix<float> rotationZ = Matrix<float>.Identity(4);
                rotationZ[0, 0] = float.Cos(float.DegreesToRadians(LocalRotation.z));
                rotationZ[0, 1] = -float.Sin(float.DegreesToRadians(LocalRotation.z));
                rotationZ[1, 0] = float.Sin(float.DegreesToRadians(LocalRotation.z));
                rotationZ[1, 1] = float.Cos(float.DegreesToRadians(LocalRotation.z));
                return rotationZ;
            }
        }

        public Vector3<float> LocalScale;
        

        public Matrix<float> LocalScaleMatrix
        {
            get
            {
                Matrix<float> result = Matrix<float>.Identity(4);
                result[0, 0] = LocalScale.x;
                result[1, 1] = LocalScale.y;
                result[2, 2] = LocalScale.z;
                return result;
            }
        }

        public Matrix<float> LocalToWorldMatrix
        {
            get
            {
                Matrix<float> localMatrix = LocalTranslationMatrix * LocalRotationMatrix * LocalScaleMatrix;
                return parent != null ? parent.LocalToWorldMatrix * localMatrix : localMatrix;
            }
        }

        public Matrix<float> WorldToLocalMatrix { 
            get { 
                Matrix<float> localToWorld = LocalToWorldMatrix;
                return localToWorld.InvertByDeterminant(); 
            } 
        }

        public Quaternion LocalRotationQuaternion
        {
            get
            {
                return Quaternion.Euler(LocalRotation.x, LocalRotation.y, LocalRotation.z);
            }

            set
            {
                LocalRotation = value.EulerAngles;
            }
        }


        public Transform()
        {
            LocalPosition = new Vector3<float>(0, 0, 0);
            LocalRotation = new Vector3<float>(0, 0, 0);
            LocalScale = new Vector3<float>(1, 1, 1);
        }

        private Transform parent;
        public void SetParent(Transform tParent)
        {
            parent = tParent;
        }
    }