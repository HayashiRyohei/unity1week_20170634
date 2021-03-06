using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class Gauss : MonoBehaviour {
	Material material;

	[SerializeField]
	[Range(0, 20)]
	int resolution = 0;

	bool _flag = false;
	float intencity = 0;
	float prevTime;

	public int Resolution { get { return resolution; } set { resolution = value; } }

	void Awake() {
		var shader = Shader.Find("Hidden/Gauss");
		material = new Material(shader);
	}

	void Update() {
		var deltaTime = Time.realtimeSinceStartup - prevTime;

		if(_flag) {
			intencity += deltaTime * 8;
		} else {
			intencity -= deltaTime * 8;
			Time.timeScale = 1;
		}
		intencity = Mathf.Clamp01(intencity);
		resolution = (int)(intencity * 10);

		prevTime = Time.realtimeSinceStartup;
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination) {
		if(resolution == 0) {
			Graphics.Blit(source, destination);
			return;
		}
		CommandBuffer command = new CommandBuffer();

		int rt1 = Shader.PropertyToID("RT1");
		command.GetTemporaryRT(rt1, -resolution, -resolution, 0, FilterMode.Point);
		int rt2 = Shader.PropertyToID("rt2");
		command.GetTemporaryRT(rt2, -resolution, -resolution, 0, FilterMode.Trilinear);

		var weight = CalcWeight(4, 8);
		command.SetGlobalVector(Shader.PropertyToID("_PixelSize"), new Vector4((float)resolution / Screen.width, (float)resolution / Screen.height, 0, 0));

		command.Blit((RenderTargetIdentifier)source, rt1, material, 0);
		command.Blit(rt1, rt2, material, 1);
		command.Blit(rt2, destination);

		Graphics.ExecuteCommandBuffer(command);
	}

	// 今回はシェーダーに定数をベタ打ちしているため未使用
	// シェーダー内ではCalcWeight(3.0f, 4)の値を使用
	float[] CalcWeight(float dispersion, int count) {
		float[] weight = new float[count];
		float total = 0;
		for(int i = 0; i < weight.Length; i++) {
			weight[i] = Mathf.Exp(-0.5f * (i * i) / dispersion);
			total += weight[i] * ((0 == i) ? 1 : 2);
		}
		for(int i = 0; i < weight.Length; ++i) weight[i] /= total;
		return weight;
	}

	/// <summary>
	/// ぼかす
	/// </summary>
	public void Blur() {
		_flag = true;
		Time.timeScale = 0f;
	}

	/// <summary>
	/// ぼかさない
	/// </summary>
	public void UnBlur() {
		_flag = false;
		Time.timeScale = 1f;
	}
}