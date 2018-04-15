using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface WhaleControllerInterface {

	void LoseEnergy (int lostEnergy);

	void ChooseSplash();

	string GetName ();

	void GotAHit(float reward);
}
