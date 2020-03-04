using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PauseTests {

    [Test]
    public void PauseGame_Test() {
        // Arrange
        //var pause = new Pause();
        var pause = new GameObject().AddComponent<Pause>().GetComponent<Pause>();
        var expectedTimeScale = 1;

        //Act
        /* Cody note: Pause.PauseGame() is of type void, so I would check that:
         * Time.timeScale == 1
         * then call 
         * Pause.PauseGame()
         * then check that
         * Time.timeScale == 0
         * 
         */

        expectedTimeScale = 1; // Game should be unpaused to start
        Assert.That(Time.timeScale, Is.EqualTo(expectedTimeScale));
        expectedTimeScale = 0; // Game should now be paused
        pause.PauseGame();
        Assert.That(Time.timeScale, Is.EqualTo(expectedTimeScale));
 
    }
}