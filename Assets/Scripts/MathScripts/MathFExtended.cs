namespace UnityEngine.MathExtensions
{
    /// <summary>
    /// @Author: Sage Derby
    /// @Date: 8/6/2022
    /// @Descrition: Adds misc math functions that other classes may need
    /// </summary>
    public static class MathFExtended
    {
        #region Math Functions
        /// <summary>
        /// Returns the sign of a number, or 0 if a zero
        /// </summary>
        /// <param name="n"></param>
        ///     Any number between - infinity and + infinity
        /// <returns></returns>
        ///     Either -1, 0, or 1 depending on it's relation to 0
        public static int SignEx(float n)
        {
            int number = 0;

            if (n < 0)
            {
                number = -1;
            }
            else if (n > 0)
            {
                number = 1;
            }
            else
            {
                number = 0;
            }

            return number;
        }
        #endregion

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

        /// <summary>
        /// Used to set numbers in a set range
        /// </summary>
        public static class Ranges
        {
            #region Range Methods
            /// <summary>
            /// Sets a number in a boundary range
            /// </summary>
            /// <param name="min"></param>
            ///     The minimum of the range inclusive
            /// <param name="x"></param>
            ///     The number being compared
            /// <param name="max"></param>
            ///     The maximum of the range inclusive
            /// <returns></returns>
            ///     A number within the bounds
            public static void InBound(float min, ref float x, float max)
            {
                //Min check
                if(x < min)
                    x = min;

                //Max check
                else if (x > max)
                    x = max;
            }

            /// <summary>
            /// Sets a number in a boundary range
            /// </summary>
            /// <param name="min"></param>
            ///     The minimum of the range inclusive
            /// <param name="x"></param>
            ///     The number being compared
            /// <param name="max"></param>
            ///     The maximum of the range inclusive
            /// <returns></returns>
            ///     A number within the bounds
            public static void IntInBound(int min, ref int x, int max)
            {
                //Min check
                if (x < min)
                    x = min;

                //Max check
                else if (x > max)
                    x = max;
            }

            /// <summary>
            /// Sets the x so that if it goes above the max bound, it loops around
            ///     and vice versa
            /// </summary>
            /// <param name="min"></param>
            ///     The minimum of the range inclusive
            /// <param name="x"></param>
            ///     The number being compared
            /// <param name="max"></param>
            ///     The maximum of the range inclusive
            /// <returns></returns>
            ///     A number within the bounds
            public static void LoopInRange(float min, ref float x, float max)
            {
                //Loop x to max if the x is minimum
                if (x < min)
                {
                    x = max;
                }

                //Loop x to min if above max
                else if (x > max)
                {
                    x = min;
                }
            }

            /// <summary>
            /// Sets the x so that if it goes above the max bound, it loops around
            ///     and vice versa
            /// </summary>
            /// <param name="min"></param>
            ///     The minimum of the range inclusive
            /// <param name="x"></param>
            ///     The number being compared
            /// <param name="max"></param>
            ///     The maximum of the range inclusive
            /// <returns></returns>
            ///     A number within the bounds
            public static void IntLoopInRange(int min, ref int x, int max)
            {
                //Loop x to max if the x is minimum
                if (x < min)
                {
                    x = max;
                }

                //Loop x to min if above max
                else if (x > max)
                {
                    x = min;
                }
            }
            #endregion
        }
    }
}

