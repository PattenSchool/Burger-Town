using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.MathExtensions;


public class SpawnFallingPlatforms : MonoBehaviour
{
    #region GameObjects and components
    [Header("Platform related")]

    [Tooltip("The prefab for the platform")]
    [SerializeField]
    private GameObject platformPrefab;

    [Tooltip("The reference to the platform that is in the ")]
    [SerializeField, HideInInspector]
    private GameObject[] platformReferences = null;

    [Tooltip("The ammount of platforms being spawned")]
    [SerializeField, Range(1, 100)]
    private int ammountOfPlatforms = 3;

    /// <summary>
    /// 
    /// </summary>
    private void GeneratePlatforms()
    {
        //TODO: Reset the references when starting the level
        platformReferences = null;
        platformReferences = new GameObject[ammountOfPlatforms];

        //TODO: Add each element for the platform when they spawn
        for(int i = 0; i < ammountOfPlatforms; i++)
        {
            //TODO: Get information on position and rotation
            float height = (i * fallDistance) / ammountOfPlatforms;
            Vector3 position = this.transform.position - (Vector3.up * height);
            Quaternion rotation = 
                GetRotationQuaternion(
                    CalculateRotationWithCorrectionalOffset(
                        GenerateSection()));

            //TODO: Spawn the platform
            platformReferences[i] = ObjectPooling.Spawn(platformPrefab, position, rotation);
        }
    }

    private void RespawnPlatform(GameObject incomingPlatform)
    {
        //Get information
        incomingPlatform.transform.position = GetSpawnCenter();
        incomingPlatform.transform.rotation = GetRotationQuaternion(
            CalculateRotationWithCorrectionalOffset(
                GenerateSection()));
    }

    /// <summary>
    /// The center of the object without any influence from the fall varaibles
    /// </summary>
    /// <returns></returns>
    private Vector3 GetSpawnCenter()
    {
        return this.transform.position;
    }

    /// <summary>
    /// The center of a circle that dictates how far the thing can fall
    /// </summary>
    /// <returns></returns>
    private Vector3 GetFallCenter()
    {
        return this.transform.position - (Vector3.up * fallDistance);
    }
    #endregion

    #region Data Variables
    [Header("Data Variables")]

    [Tooltip("The distance that the platform can fall in meters")]
    [SerializeField, Min(0f)]
    private float fallDistance = 1f;
    #endregion

    #region Rotation Methods
    [Header("Rotation Related")]

    [Tooltip("The number of sections that the platform can spawn in,\n" +
        "360 degrees / number of sections = what angles the platform can " +
        "spawn in, in degrees")]
    [SerializeField, Range(1, 10)]
    private int numberOfSections = 6;

    [Tooltip("The ammount of offset in degrees")]
    [SerializeField, Range(-360f, 360f)]
    private float degreeOffset = 0f;

    [Tooltip("Correctional offset")]
    [SerializeField, HideInInspector]
    private float correctionOffset = -14.81f;

    [Tooltip("The last known spawn section of the platform")]
    [SerializeField, HideInInspector]
    private int lastKnownSection = 1;

    /// <summary>
    /// Generates the rotation of the next platform in degrees
    /// </summary>
    /// <returns></returns>
    private float CalculateRotation(int spawnedSection)
    {
        //TODO: Start the test result, even in case of a null
        float result;

        #region Generate Actual rotation
        //TODO: Get the entire circle in degrees
        float fullCircleAngle = 360f;

        //TODO: Set the result to the spawned section
        result = (fullCircleAngle * spawnedSection) / (numberOfSections);

        //TODO: Apply the offset to the result
        result += degreeOffset;

        #endregion
        //TODO: Return the result
        return result;
    }

    /// <summary>
    /// Calculate with the correctional angle in 
    /// </summary>
    /// <param name="spawnedSection"></param>
    /// <returns></returns>
    private float CalculateRotationWithCorrectionalOffset(int spawnedSection)
    {
        return CalculateRotation(spawnedSection) + correctionOffset;
    }

    /// <summary>
    /// Returns a random section between [1, maxSections]
    ///     between 1 and maxSections inclusive to both
    /// </summary>
    /// <returns></returns>
    private int GenerateSection()
    {
        int result = 0;

        int variance = 2;
        int randomizedVariance = Random.Range(1, variance + 1);
        int randomizedSign = MathFExtended.RandomExtended.RandomSign();
        int randomizedChange = randomizedVariance * randomizedSign;

        result += (lastKnownSection + randomizedChange + (numberOfSections))
            % numberOfSections;

        lastKnownSection = result;
        //TODO: Generate the random section
        //result = Random.Range(1, numberOfSections + 1);
        return result;
    }

    /// <summary>
    /// Returns a quaternion angle of the rotation around the y axis only in degrees
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    private Quaternion GetRotationQuaternion(float angle)
    {
        Quaternion result = Quaternion.identity;
        result = Quaternion.Euler(0f, angle, 0f);
        return result;
    }
    #endregion

    #region Unity Methods
    private void Start()
    {
        GeneratePlatforms();
    }

    private void Update()
    {
        for(int i = 0; i < ammountOfPlatforms; i++)
        {
            //The referenced platform
            GameObject platform = platformReferences[i];

            //Check height
            if (platform.transform.position.y <= GetFallCenter().y)
            {
                //Rsepawn platform if lower than allowed position
                RespawnPlatform(platform);
            }
        }
    }
    #endregion

    #region Debug Methods
    [Header("Debug")]

    [Tooltip("The color the spawns will go in to differentiate the possible" +
        " spawns")]
    [SerializeField]
    private Color debugColor = Color.white;

    [Tooltip("Possible radius in meters of the spawn (for debug)")]
    [SerializeField]
    private float spawnRadius = 1f;

    private void OnDrawGizmos()
    {
        Gizmos.color = debugColor;

        for (int i = 1; i <= ammountOfPlatforms; i++)
        {
            float height = (fallDistance * i) / ammountOfPlatforms;
            DrawPlatformSpawnLocations(height);
        }
            
    }

    /// <summary>
    /// Draws where the platforms will spawn
    /// </summary>
    private void DrawPlatformSpawnLocations(float lowestHeight)
    {
        //TODO: Generate lines for each section
        for (int i = 1; i <= numberOfSections; i++)
        {
            //TODO: Generate line from center, to falldistance, to spawn radius of center, the
            //TODO: spawn radius of falldistance
            //TODO: Find the points being drawn to
            float sectionAngle = CalculateRotation(i);
            Vector3 spawnDirection = (new Vector3(
                    Mathf.Cos(sectionAngle * Mathf.Deg2Rad),
                    0f,
                    Mathf.Sin(sectionAngle * Mathf.Deg2Rad))) * spawnRadius;
            Vector3 centerOriginal = this.transform.position;
            Vector3 centerFallHeight = this.transform.position - (Vector3.up * lowestHeight);
            Vector3 spawnOriginal = centerOriginal + spawnDirection;
            Vector3 spawnFallHeight = centerFallHeight + spawnDirection;

            //TODO: Draw the square for the section
            DrawSquare(spawnFallHeight, spawnOriginal, centerOriginal, centerFallHeight);
        }
    }

    /// <summary>
    /// Draws a squarewith an x in the middle
    /// </summary>
    /// <param name="corner1"></param>
    /// <param name="corner2"></param>
    /// <param name="corner3"></param>
    /// <param name="corner4"></param>
    private void DrawSquare(Vector3 corner1, Vector3 corner2,
        Vector3 corner3, Vector4 corner4)
    {
        Gizmos.DrawLine(corner1, corner2);
        Gizmos.DrawLine(corner2, corner3);
        Gizmos.DrawLine(corner3, corner4);
        Gizmos.DrawLine(corner4, corner1);
    }
    #endregion

    #region Legacy 2.0
    //#region GameObjects and components
    //[Header("Gameobjects and Components")]

    //[Tooltip("The prefab for the platform")]
    //[SerializeField]
    //private GameObject platformPrefab;

    //[Tooltip("The reference to the platform that is in the ")]
    //[SerializeField, HideInInspector]
    //private GameObject platformReference = null;

    //private void GeneratePlatform()
    //{
    //    if (platformReference == null)
    //    {
    //        platformReference = GameObject.Instantiate(platformPrefab);
    //    }

    //    platformReference.transform.position = GetSpawnCenter();
    //    platformReference.transform.rotation =
    //        GetRotationQuaternion(
    //            CalculateRotationWithCorrectionalOffset(
    //                GenerateSection()));
    //}

    //private Quaternion GetRotationQuaternion(float angle)
    //{
    //    Quaternion result = Quaternion.identity;
    //    result = Quaternion.Euler(0f, angle, 0f);
    //    return result;
    //}

    //private Vector3 GetSpawnCenter()
    //{
    //    return this.transform.position;
    //}

    ///// <summary>
    ///// The center of a circle that dictates how far the thing can fall
    ///// </summary>
    ///// <returns></returns>
    //private Vector3 GetFallCenter()
    //{
    //    return this.transform.position - (Vector3.up * fallDistance);
    //}
    //#endregion

    //#region Data Variables
    //[Header("Data Variables")]

    //[Tooltip("The distance that the platform can fall in meters")]
    //[SerializeField, Min(0f)]
    //private float fallDistance = 1f;
    //#endregion

    //#region Rotation
    //[Header("Rotation Related")]


    //[Tooltip("The number of sections that the platform can spawn in,\n" +
    //    "360 degrees / number of sections = what angles the platform can " +
    //    "spawn in, in degrees")]
    //[SerializeField, Min(1)]
    //private int numberOfSections = 6;

    //[Tooltip("The ammount of offset in degrees")]
    //[SerializeField, Range(-360f, 360f)]
    //private float degreeOffset = 0f;

    //[Tooltip("Correctional offset")]
    //[SerializeField, HideInInspector]
    //private float correctionOffset = -16.16f;

    ///// <summary>
    ///// Generates the rotation of the next platform in degrees
    ///// </summary>
    ///// <returns></returns>
    //private float CalculateRotation(int spawnedSection)
    //{
    //    //TODO: Start the test result, even in case of a null
    //    float result;

    //    #region Generate Actual rotation
    //    //TODO: Get the entire circle in degrees
    //    float fullCircleAngle = 360f;

    //    //TODO: Set the result to the spawned section
    //    result = (fullCircleAngle * spawnedSection) / (numberOfSections);

    //    //TODO: Apply the offset to the result
    //    result += degreeOffset;

    //    #endregion
    //    //TODO: Return the result
    //    return result;
    //}

    //private float CalculateRotationWithCorrectionalOffset(int spawnedSection)
    //{
    //    return CalculateRotation(spawnedSection) + correctionOffset;
    //}

    ///// <summary>
    ///// Returns a random section between [1, maxSections]
    /////     between 1 and maxSections inclusive to both
    ///// </summary>
    ///// <returns></returns>
    //private int GenerateSection()
    //{
    //    int result = 0;

    //    //TODO: Generate the random section
    //    result = Random.Range(1, numberOfSections + 1);

    //    return result;
    //}
    //#endregion

    //#region Unity Methods
    //private void Start()
    //{
    //    //TODO: Spawn the platform
    //    GeneratePlatform();


    //}

    //private void Update()
    //{
    //    float currentPlatformHeight = platformReference.transform.position.y;

    //    //TODO: Check if platform is below height
    //    if (currentPlatformHeight <= GetFallCenter().y)
    //    {
    //        //TODO: If platform is below height, reset to regular height
    //        //TODO: Randomize section spawned
    //        GeneratePlatform();
    //    }
    //}
    //#endregion

    //#region Debug
    //[Header("Debug")]

    //[Tooltip("Possible radius in meters of the spawn (for debug)")]
    //[SerializeField]
    //private float spawnRadius = 1f;

    //[Tooltip("The color the spawns will go in to differentiate the possible" +
    //    " spawns")]
    //[SerializeField]
    //private Color debugColor = Color.white;

    //private void OnDrawGizmos()
    //{
    //    //TODO: Generate the gizmos color
    //    Gizmos.color = debugColor;

    //    //TODO: Generate lines for each section
    //    for(int i = 1; i <= numberOfSections; i++)
    //    {
    //        //TODO: Generate line from center, to falldistance, to spawn radius of center, the
    //        //TODO: spawn radius of falldistance
    //        //TODO: Find the points being drawn to
    //        float sectionAngle = CalculateRotation(i);
    //        Vector3 spawnDirection = (new Vector3(
    //                Mathf.Cos(sectionAngle * Mathf.Deg2Rad),
    //                0f,
    //                Mathf.Sin(sectionAngle * Mathf.Deg2Rad))) * spawnRadius;
    //        Vector3 centerOriginal = this.transform.position;
    //        Vector3 centerFallHeight = this.transform.position - (Vector3.up * fallDistance);
    //        Vector3 spawnOriginal = centerOriginal + spawnDirection;
    //        Vector3 spawnFallHeight = centerFallHeight + spawnDirection;

    //        //TODO: Draw the square for the section
    //        DrawSquare(centerOriginal, centerFallHeight,
    //            spawnOriginal, spawnFallHeight);
    //        Gizmos.DrawLine(centerOriginal, spawnOriginal);
    //        Gizmos.DrawLine(centerFallHeight, spawnFallHeight);
    //    }
    //}

    //private void DrawSquare(Vector3 corner1, Vector3 corner2, 
    //    Vector3 corner3, Vector4 corner4)
    //{
    //    Gizmos.DrawLine(corner1, corner2);
    //    Gizmos.DrawLine(corner2, corner3);
    //    Gizmos.DrawLine(corner3, corner4);
    //    Gizmos.DrawLine(corner4, corner1);
    //}
    //#endregion
    #endregion

    #region Legacy

    //#region GameObject reference
    //[Header("GameObjects")]

    //[Tooltip("The platform to be spawned in")]
    //[SerializeField]
    //private GameObject spawnablePlatform;
    //#endregion

    //#region Data Related
    //[Header("Data Variables")]

    ////[Tooltip("The ammount of time in seconds between platforms")]
    ////[SerializeField, Min(0.1f)]
    ////private float spawnDelay = 1.0f;

    ////[Tooltip("The time remaining until next spawn")]
    ////[SerializeField, Min(0f)]
    ////private float spawnTimeRemaining = 0f;

    //[Tooltip("The distance above the player that platofrms will spawn" +
    //    " in meters")]
    //[SerializeField, Min(0f)]
    //private float spawnDistanceFromStart = 0f;
    //#endregion

    //#region Rotation related
    //[Header("Rotation Generation")]

    //[Tooltip("The ammount of sections devided of the circle, " +
    //    "where possible spawns are. This = 360 degrees")]
    //[SerializeField, Min(1)]
    //private int maxSections = 6;

    //[Tooltip("The offset of each generation of a platform")]
    //[SerializeField, Range(-360f, 360)]
    //private float degreeOffset = 0f;

    ///// <summary>
    ///// Generates the rotation of the next platform in degrees
    ///// </summary>
    ///// <returns></returns>
    //private float CalculateRotation(int spawnedSection)
    //{
    //    //TODO: Start the test result, even in case of a null
    //    float result = 0f;

    //    #region Generate Actual rotation
    //    //TODO: Get the entire circle in degrees
    //    float fullCircleAngle = 360f;

    //    //TODO: Set the result to the spawned section
    //    result = fullCircleAngle / (spawnedSection);

    //    //TODO: Apply the offset to the result
    //    result += degreeOffset;

    //    #endregion
    //    //TODO: Return the result
    //    return result;
    //}

    ///// <summary>
    ///// Returns a random section between [1, maxSections]
    /////     between 1 and maxSections inclusive to both
    ///// </summary>
    ///// <returns></returns>
    //private int GenerateSection()
    //{
    //    int result = 0;

    //    //TODO: Generate the random section
    //    result = Random.Range(1, maxSections + 1);

    //    return result;
    //}
    //#endregion

    //#region Unity Methods
    //private void Update()
    //{
    //    //if (spawnTimeRemaining <= 0f)
    //    //{
    //    //    //TODO: Spawn new platform 
    //    //    Vector3 spawnPos = (Vector3.up * (spawnDistanceFromStart + this.transform.position.y));
    //    //    float randomRotation = CalculateRotation(GenerateSection());
    //    //    Quaternion rotation = Quaternion.Euler(0f, randomRotation, 0f);

    //    //    var platform = ObjectPooling.Spawn(spawnablePlatform, spawnPos, rotation);
    //    //    platform.transform.parent = this.gameObject.transform;
    //    //    platform.transform.position = spawnPos;
    //    //    //TODO: Reset timer
    //    //    spawnTimeRemaining = spawnDelay;
    //    //}
    //    //else
    //    //{
    //    //    spawnTimeRemaining -= Time.deltaTime;
    //    //}

    //}
    //#endregion

    //#region Debug Options
    //private void OnDrawGizmos()
    //{
    //    #region Generate Spawn height Cubes
    //    Vector3 spawnCenter = new Vector3(
    //        this.transform.position.x,
    //        this.transform.position.y + spawnDistanceFromStart,
    //        this.transform.position.z);
    //    Vector3 currentPos = this.transform.position;
    //    float lineLength = 4f;
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawLine(spawnCenter, spawnCenter + (Vector3.left * lineLength));
    //    Gizmos.DrawLine(spawnCenter, spawnCenter + (Vector3.right * lineLength));
    //    Gizmos.DrawLine(spawnCenter, spawnCenter + (Vector3.forward * lineLength));
    //    Gizmos.DrawLine(spawnCenter, spawnCenter + (Vector3.back * lineLength));
    //    Gizmos.DrawLine(spawnCenter, currentPos);
    //    Gizmos.DrawLine(currentPos, currentPos + (Vector3.left * lineLength));
    //    Gizmos.DrawLine(currentPos, currentPos + (Vector3.right * lineLength));
    //    Gizmos.DrawLine(currentPos, currentPos + (Vector3.forward * lineLength));
    //    Gizmos.DrawLine(currentPos, currentPos + (Vector3.back * lineLength));
    //    Gizmos.DrawLine(currentPos, currentPos);
    //    Gizmos.DrawLine(spawnCenter + (Vector3.left * lineLength), currentPos + (Vector3.left * lineLength));
    //    Gizmos.DrawLine(spawnCenter + (Vector3.right * lineLength), currentPos + (Vector3.right * lineLength));
    //    Gizmos.DrawLine(spawnCenter + (Vector3.forward * lineLength), currentPos + (Vector3.forward * lineLength));
    //    Gizmos.DrawLine(spawnCenter + (Vector3.back * lineLength), currentPos + (Vector3.back * lineLength));
    //    #endregion

    //    #region Generate possible sections
    //    //TODO: Give the gizmos a color
    //    Gizmos.color = Color.blue;

    //    //TODO: Define direction
    //    float directionRadius = 1f;

    //    //TODO: Draw a line showing possible sections
    //    for(int i = 0; i < maxSections; i++)
    //    {
    //        //TODO: Generate vector of direction
    //        float directionAngle = CalculateRotation(i + 1);



    //        ////TODO: Calculate the vectors needed
    //        //Vector3 center = this.transform.position;
    //        //Vector3 spawnHeightPosition = this.transform.position + new Vector3(0f, spawnDistanceFromStart);
    //        //Vector3 directionOnFloor = Mathf.Cos(directionAngle * Mathf.Deg2Rad);

    //        ////TODO: Draw line from center to direction
    //        //Gizmos.DrawLine();

    //        //TODO: Draw line from spawn height to direction

    //        //TODO: Draw line from spawn height direction to center direction

    //    }
    //    #endregion
    //}
    //#endregion
    #endregion
}