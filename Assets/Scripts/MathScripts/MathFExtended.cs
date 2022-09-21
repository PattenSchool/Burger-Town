namespace UnityEngine.MathExtensions
{
    /// <summary>
    /// @Author: Sage Derby
    /// @Date: 8/6/2022
    /// @Descrition: Adds misc math functions that other classes may need
    /// </summary>
    public static class MathFExtended
    {
        /// <summary>
        /// The class of rotation and vector methods
        /// </summary>
        public static class RotationAndVectorMethods
        {
            #region Math Constants
            //A constant that converts degrees to radians 
            private const float DEGREES_TO_RAD_CONST = Mathf.PI / 180f;

            //A constant that converts radians to degrees
            private const float RAD_TO_DEGREES_CONST = 180f / Mathf.PI;
            #endregion

            #region Rotation Methods
            /// <summary>
            /// Converts the rotation to radians
            /// </summary>
            /// <param name="rotationInDegrees"></param>
            ///     The initial rotation in degrees
            /// <returns></returns>
            ///     The rotation in radians
            public static float ConvertToRadians(float rotationInDegrees)
            {
                //Calculate the angle to radians
                float rotationInRadians = rotationInDegrees * DEGREES_TO_RAD_CONST;

                //Return the result in radians
                return rotationInRadians;
            }

            /// <summary>
            /// Converts a rotation in radians to degrees
            /// </summary>
            /// <param name="rotationInRadians"></param>
            ///     The current rotation in radians
            /// <returns></returns> 
            ///     The rotation in degrees
            public static float ConvertToDegrees(float rotationInRadians)
            {
                //Calculate the angle to degrees
                float rotationInDegrees = rotationInRadians * RAD_TO_DEGREES_CONST;

                //Return the result in degrees
                return rotationInDegrees;
            }
            #endregion

            #region Vector Methods
            /// <summary>
            /// Converts a given angle to a direction vector based on Vector2.right
            /// </summary>
            /// <param name="rotation"></param>
            ///     The rotation of an object
            /// <param name="isDegree"></param>
            ///     Asks if the measure is in degrees
            /// <returns></returns>
            ///     A direction of a vector compared to the Vector2.right
            public static Vector2 ConvertRotationToVector(float rotation, bool isDegree)
            {
                //Make sure the vector was in radians
                if (isDegree == true)
                    rotation = ConvertToRadians(rotation);

                //Convert the rotation to a vector
                Vector2 directionVector = new Vector2(Mathf.Cos(rotation), Mathf.Sin(rotation));

                //Return the result
                return directionVector;
            }
            #endregion
        }
    }
}

